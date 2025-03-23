using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CarAnimator))]
public class CarMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private MapLogic _map;
    [SerializeField] private ScoreCounter _counter;
    [SerializeField] private CarTextViewer _viewer;
    [SerializeField] private AnimationCurve _animCurve;
    [SerializeField] private CarMoverEffector _effector;
    [SerializeField] private List< WheelEffectViewer > _trailEffects;
    [SerializeField] private Outline _outliner;

    private CarAnimator _animator;
    private TileHelper _startPositionTile;
    private TileHelper _finishPositionTile;

    private bool _isMoving;
    private bool _inParking;

    private Namer _parkPlace;
    private Coroutine _moving;
    private bool _isSelected;

    private WaitForSeconds _wait1Millisecond;
    private float _delay1Millisecond = 0.1f;
    private WaitForSeconds _wait15Millisecond;
    private float _delay15Millisecond = 1.5f;

    private void OnEnable()
    {
        _outliner.OutlineWidth = 2;
        _animator = GetComponent<CarAnimator>();
        SetWaitings();
        _isMoving = false;
        _inParking = false;
        _isSelected = false;
        _moving = null;
        _counter.AddMaxScore(1);
    }

    private void OnDisable()
    {
        StopCoroutine(MovingInQuenue());
        _moving = null;
        _startPositionTile = null;
        _finishPositionTile = null;
    }

    private void SetWaitings()
    {
        _wait1Millisecond = new WaitForSeconds(_delay1Millisecond);
        _wait15Millisecond = new WaitForSeconds(_delay15Millisecond);
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
                yield return _wait1Millisecond;
            }
            else
            {
                _map.AddObstacle(_startPositionTile.cordX, _startPositionTile.cordY);
                yield return _wait15Millisecond;
            }
        }

        _viewer.ActivateBackground();

        foreach (var item in _trailEffects)
        {
            item.DrawLine();
        }

        if (transform.position == _map.GetTile(_startPositionTile.cordX, _map.RoadOffVerticalValue).transform.position)
        {
            _outliner.OutlineWidth = 8;
            _isMoving = false;
        }
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
            _animator.WrongAnimationStart();
            _map.AddObstacle(start.cordX, start.cordY);
            _isMoving = false;
            return;
        }

        if (_inParking)
        {
            _map.AddObstacle(_startPositionTile.cordX, _startPositionTile.cordY);
            _counter.RemoveScore();
        }
        else
        {
            _outliner.OutlineWidth = 0;
            _map.AddObstacle(_finishPositionTile.cordX, _finishPositionTile.cordY);
        }

        Instantiate(_effector.PlaySmoke(), transform);

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
        for (int i = 0; i < targets.Count; i++)
        {
            float count = targets.Count;

            while (transform.position != targets[i].transform.position)
            {
                float timeLerp = Math.Clamp(1, 0, i / count);
                float speed = _animCurve.Evaluate(timeLerp);
                float step = speed * Time.deltaTime;

                transform.position = Vector3.MoveTowards(transform.position, targets[i].transform.position, step);
                transform.LookAt(targets[i].transform);

                yield return null;
             
                foreach (var item in _trailEffects)
                {
                    item.DrawLine();
                }
            }
        }

        if (targets[targets.Count - 1].cordY > _map.RoadOffVerticalValue)
            _inParking = true;
        else
            _inParking = false;

        OnFinishPosition();
    }

    private void OnFinishPosition()
    {
        if (_inParking)
        {
            _viewer.DeactivateBackground();
            _map.AddObstacle(_finishPositionTile.cordX, _finishPositionTile.cordY);
            _counter.AddScore();
        }
        else
        {
            _map.RemoveObstacle(_finishPositionTile.cordX, _finishPositionTile.cordY);
            StartCoroutine(MovingInQuenue());
            transform.LookAt(_map.GetTile(_startPositionTile.cordX, _startPositionTile.cordY + 2).transform);
            _viewer.ActivateBackground();
        }

        _isSelected = false;
        _isMoving = false;
    }
}
