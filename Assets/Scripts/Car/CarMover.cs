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

        if (CheckPosition(_startPositionTile.cordX, _startPositionTile.cordY + 1) == false)
            return;

        _isSelected = true;


        if (_inParking)
        {
            _startPositionTile = _map.GetFreePlaceOnQenue(_startPositionTile.cordX);
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

        while (_startPositionTile.cordY < _map.RoadOffVerticalValue)
        {
            if (CheckPosition(_startPositionTile.cordX, _startPositionTile.cordY + 1))
            {
                TeleportTo(_startPositionTile.cordY + 1);
                yield return new WaitForSeconds(0.1f);
            }
            else
            {
                yield return new WaitForSeconds(1.5f);
            }
        }

        _viewer.ActivateBackground();
        _isMoving = false;
    }

    private void TeleportTo(int coordY)
    {
        TileHelper tile = _map.GetTile(_startPositionTile.cordX, coordY);
        _map.RemoveObstacle(_startPositionTile.cordX, _startPositionTile.cordY);

        transform.LookAt(tile.transform.position);
        transform.position = tile.transform.position;
        _startPositionTile = tile;
        _map.AddObstacle(_startPositionTile.cordX, _startPositionTile.cordY);

    }

    private void TryMoving(TileHelper start, TileHelper end)
    {
        _isMoving = true;

        _map.RemoveObstacle(start.cordX, start.cordY);
        _map.SetStart(end);
        _map.SetEnd(start);

        List<TileHelper> hashPath = _map.GetPath();

        if (hashPath == null || IsPathCorrect(hashPath) == false)
        {
            _map.AddObstacle(start.cordX, start.cordY);
            _isMoving = false;
            return;
        }


        if (_inParking)
            _counter.RemoveScore();
        else
            _map.AddObstacle(_finishPositionTile.cordX, _finishPositionTile.cordY);

        _moving = StartCoroutine(MoveToPath(hashPath));
        hashPath = new List<TileHelper>();
    }

    private bool IsPathCorrect(List<TileHelper> hashPath)
    {
        foreach (TileHelper item in hashPath)
            if (_map.CheckObstacle(item.cordX, item.cordY))
                return false;

        return true;
    }

    private IEnumerator MoveToPath(List<TileHelper> targets)
    {
        float step = _speed * Time.fixedDeltaTime;

        for (int i = 0; i < targets.Count; i++)
        {
            while (transform.position != targets[i].transform.position)
            {
                transform.position = Vector3.MoveTowards(transform.position, targets[i].transform.position, step);
                transform.LookAt(targets[i].transform);

                yield return null;
            }
        }

        OnFinishPosition();
    }


    private void OnFinishPosition()
    {
        if (_isSelected)
        {
            _inParking = !_inParking;
        }

        if (_inParking)
        {
            _viewer.DeactivateBackground();
            _map.AddObstacle(_finishPositionTile.cordX, _finishPositionTile.cordY);
            transform.position = _finishPositionTile.transform.position;
            _counter.AddScore();
        }
        else
        {
            _map.RemoveObstacle(_finishPositionTile.cordX, _finishPositionTile.cordY);
            StartCoroutine(MovingInQuenue());
            transform.LookAt(_map.GetTile(_startPositionTile.cordX, _startPositionTile.cordY + 2).transform);
            _viewer.ActivateBackground();
            transform.position = _startPositionTile.transform.position;
        }

        _isSelected = false;
        _isMoving = false;
    }
}
