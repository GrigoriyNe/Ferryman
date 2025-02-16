using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Car : MonoBehaviour //, IMoveable
{
    [SerializeField] private float _speed;
    [SerializeField] private Map _map;
    [SerializeField] private TextMeshProUGUI _viewFinishPosition;
    [SerializeField] private Image _backgroundText;
    [SerializeField] private NamesOfParkingPlaces _placesName;
    [SerializeField] private ScoreCounter _counter;

    private TileHelper _startPositionTile;
    private TileHelper _finishPositionTile;

    private bool _isMoving;
    private bool _inParking;
    private bool _isNextPositionEmpty;

    private Coroutine _moving;
    private bool _isSelected;

    private void OnEnable()
    {
        _isMoving = false;
        _inParking = false;
        _isNextPositionEmpty = false;
        _isSelected = false;
        _moving = null;
    }

    private void OnDisable()
    {
        _startPositionTile = null;
        _finishPositionTile = null;
        _backgroundText.gameObject.SetActive(true);
    }

    public void Init(TileHelper startPositionTile, TileHelper finishPositionTile)
    {
        _startPositionTile = startPositionTile;
        _finishPositionTile = finishPositionTile;
        transform.position = _startPositionTile.transform.position;
        _viewFinishPosition.text = GetTextPosition();

        StartCoroutine(MovingInQuenue());
    }

    private IEnumerator MovingInQuenue()
    {
        while (_startPositionTile.cord_y < 8)
        {

            if (CheckPosition(_startPositionTile.cord_x, _startPositionTile.cord_y + 1))
            {
                _isMoving = true;

                _map.RemoveObstacle(_startPositionTile.cord_x, _startPositionTile.cord_y);
                transform.position = _map.GetTile(_startPositionTile.cord_x, _startPositionTile.cord_y + 1).transform.position;
                _startPositionTile = _map.GetTile(_startPositionTile.cord_x, _startPositionTile.cord_y + 1);
                _map.AddObstacle(_startPositionTile.cord_x, _startPositionTile.cord_y);
            }

            yield return new WaitForSeconds(0.6f);
        }

        _isMoving = false;
    }

    private bool CheckPosition(int x, int y)
    {
        return !_map.CheckObstacle(x, y);
    }

    private string GetTextPosition()
    {
        return _placesName.GetTextPlace(_finishPositionTile.cord_x, _finishPositionTile.cord_y);
    }

    public void Move()
    {
        if (_isMoving)
            return;

        if (_isNextPositionEmpty)
            return;

        _isSelected = true;


        if (_inParking)
        {
            TryMoving(_finishPositionTile, _startPositionTile);

        }
        else
        {
            TryMoving(_startPositionTile, _finishPositionTile);
        }
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

        if (_inParking)
        {
            _counter.RemoveScore();
        }
        else
        {
            _map.AddObstacle(_finishPositionTile.cord_x, _finishPositionTile.cord_y);
        }

        _backgroundText.gameObject.SetActive(false);
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
            _startPositionTile = _map.GetTile(_startPositionTile.cord_x, 0);
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
            _map.AddObstacle(_startPositionTile.cord_x, _startPositionTile.cord_y);
            StartCoroutine(MovingInQuenue());
            transform.LookAt(_map.GetTile(_startPositionTile.cord_x, _startPositionTile.cord_y + 1).transform);
            _backgroundText.gameObject.SetActive(true);
            transform.position = _startPositionTile.transform.position;

        }
    }
}
