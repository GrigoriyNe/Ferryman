using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private MapLogic _map;
    [SerializeField] private ScoreCounter _counter;
    [SerializeField] private CarTextViewer _viewer;

    private TileHelper _startPositionTile;
    private TileHelper _finishPositionTile;

    private bool _isMoving;
    private bool _inParking;

    private Namer _parkPlace;
    private Coroutine _moving;
    private bool _isSelected;

    private void OnEnable()
    {
        _isMoving = false;
        _inParking = false;
        _isSelected = false;
        _moving = null;
        _counter.AddMaxScore(1);
    }

    private void OnDisable()
    {
        _startPositionTile = null;
        _finishPositionTile = null;
    }

    public void MoveInQuenue()
    {
        StartCoroutine(MovingInQuenue());
    }

    public void Init(TileHelper startPositionTile, TileHelper finishPositionTile, Namer parkPlace)
    {
        _parkPlace = parkPlace;
        _startPositionTile = startPositionTile;
        _finishPositionTile = finishPositionTile;
    }

    public void Move()
    {
        if (_isMoving)
            return;

        if (CheckPosition(_startPositionTile.cord_x, _startPositionTile.cord_y + 1) == false)
            return;

        _isSelected = true;


        if (_inParking)
        {
            _startPositionTile = _map.GetFreePlaceOnQenue(_startPositionTile.cord_x);
            TryMoving(_finishPositionTile, _startPositionTile);
        }
        else
        {
            TryMoving(_startPositionTile, _finishPositionTile);
        }
    }

    private bool CheckPosition(int x, int y)
    {
        return !_map.CheckObstacle(x, y);
    }

    private IEnumerator MovingInQuenue()
    {
        _isMoving = true;

        while (_startPositionTile.cord_y < _map.RoadOffVerticalValue)
        {
            if (CheckPosition(_startPositionTile.cord_x, _startPositionTile.cord_y + 1))
            {
                TeleportTo(_startPositionTile.cord_y + 1);
                yield return new WaitForSeconds(0.1f);
            }
            else
            {
                yield return new WaitForSeconds(2f);
            }
        }

        _viewer.ActivateBackground();
        _isMoving = false;
    }

    private void TeleportTo(int coordY)
    {
        TileHelper tile = _map.GetTile(_startPositionTile.cord_x, coordY);
        _map.RemoveObstacle(_startPositionTile.cord_x, _startPositionTile.cord_y);

        transform.LookAt(tile.transform.position);
        transform.position = tile.transform.position;
        _startPositionTile = tile;
        _map.AddObstacle(_startPositionTile.cord_x, _startPositionTile.cord_y);

    }

    private void TryMoving(TileHelper start, TileHelper end)
    {
        _isMoving = true;

        _map.RemoveObstacle(start.cord_x, start.cord_y);
        _map.SetStart(end);
        _map.SetEnd(start);
        
        if (_map.GetPath() == null)
        {
            _map.AddObstacle(start.cord_x, start.cord_y);
            _isMoving = false;
            return;
        }

        _viewer.DeactivateBackground();

        if (_inParking)
        {
            _map.RemoveObstacle(_finishPositionTile.cord_x, _finishPositionTile.cord_y);
            _counter.RemoveScore();
        }
        else
        {
          //  _map.AddObstacle(_finishPositionTile.cord_x, _finishPositionTile.cord_y);
        }

        _moving = StartCoroutine(MoveToPath(_map.GetPath()));
    }

    private IEnumerator MoveToPath(List<TileHelper> targets)
    {
        float step = _speed * Time.deltaTime;

        for (int i = 0; i < targets.Count; i++)
        {
            while (transform.position != targets[i].transform.position)
            {
                transform.position = Vector3.MoveTowards(transform.position, targets[i].transform.position, step);
                transform.LookAt(targets[i].transform);

                yield return null;
            }
        }

        _isMoving = false;

        OnFinishPosition();
    }


    private void OnFinishPosition()
    {
        if (_isSelected)
        {
            _inParking = !_inParking;
        }

        _isSelected = false;

        if (_inParking)
        {
            transform.position = _finishPositionTile.transform.position;
            _map.AddObstacle(_finishPositionTile.cord_x, _finishPositionTile.cord_y);
            _counter.AddScore();
        }
        else
        {
            _map.RemoveObstacle(_finishPositionTile.cord_x, _finishPositionTile.cord_y);
            _map.AddObstacle(_startPositionTile.cord_x, _startPositionTile.cord_y);
            StartCoroutine(MovingInQuenue());
            transform.LookAt(_map.GetTile(_startPositionTile.cord_x, _startPositionTile.cord_y + 2).transform);
            _viewer.ActivateBackground();
            transform.position = _startPositionTile.transform.position;
        }
    }
}
