using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CarAnimator))]
public class CarMover : MonoBehaviour
{
    private const int LowerSteps = 1;
    private const int Offset = 1;
    private const float MinValueForChechObstacleOnQuenue = 0.2f;
    private const float MaxValueForChechObstacleOnQuenue = 1f;
    private const float MiddleTransform = 10;

    [SerializeField] private float _speed;
    [SerializeField] private MapLogic _map;
    [SerializeField] private ScoreCounter _counter;
    [SerializeField] private CarTextViewer _viewer;
    [SerializeField] private AnimationCurve _animCurve;
    [SerializeField] private CarMoverEffector _effector;
    [SerializeField] private List<WheelEffectViewer> _trailEffects;
    [SerializeField] private Outline _outliner;
    [SerializeField] private Transform transformPipe;
    [SerializeField] private List<MeshRenderer> _meshRenderers;

    private CarAnimator _animator;
    private TileHelper _startPositionTile;
    private TileHelper _finishPositionTile;

    private bool _isMoving;
    private bool _inParking;
    private bool _isEmptyFinisPlace;

    private Namer _parkPlace;
    private Coroutine _moving;
    private bool _isSelected;

    private WaitForSeconds _wait1Millisecond;
    private float _delay1Millisecond = 0.1f;
    private WaitForSeconds _waitStart;
    private float _waitStartValue = 0.5f;
    private float _movingQuenueMultiplicateValue = 2f;

    private void OnEnable()
    {
        _outliner.enabled = false;
        _animator = GetComponent<CarAnimator>();
        SetWaitings();
        _isEmptyFinisPlace = true;
        _isMoving = false;
        _inParking = false;
        _isSelected = false;
        _moving = null;
    }

    private void OnDisable()
    {
        _isEmptyFinisPlace = true;
        StopCoroutine(ChangingPositionOnQuenue());
        StopCoroutine(MovingAway());
        _moving = null;
        _startPositionTile = null;
        _finishPositionTile = null;
    }

    public void Init(TileHelper startPositionTile, TileHelper finishPositionTile, Namer parkPlace)
    {
        _parkPlace = parkPlace;
        _startPositionTile = startPositionTile;
        _finishPositionTile = finishPositionTile;
    }

    public void MoveInQuenue()
    {
        StartCoroutine(ChangingPositionOnQuenue());
    }

    public void Move()
    {
        if (_isMoving)
            return;

        if (_counter.ScoreLess <= LowerSteps)
            return;

        _isSelected = true;

        if (_inParking)
        {
            _startPositionTile = _map.GetFreePlaceOnQenue(_startPositionTile.CordX);

            TryMoving(_finishPositionTile, _startPositionTile);
        }
        else
        {
            if (IsPositionEmpty(_startPositionTile.CordX, _startPositionTile.CordY + Offset) == false)
            {
                _animator.WrongAnimationStart();

                return;
            }

            if (IsPositionEmpty(_finishPositionTile.CordX, _finishPositionTile.CordY) == false)
            {
                _isEmptyFinisPlace = false;
                MoveToZeroPosition();

                return;
            }

            TryMoving(_startPositionTile, _finishPositionTile);
        }
    }

    public void MoveAway()
    {
        if (_inParking == false)
            StartCoroutine(MovingAway());
    }

    private void SetWaitings()
    {
        _wait1Millisecond = new WaitForSeconds(_delay1Millisecond);
        _waitStart = new WaitForSeconds(_waitStartValue);
    }

    private void MoveToZeroPosition()
    {
        _animator.TurnAnimationStart();
        OnMoving();
        _map.RemoveObstacle(_startPositionTile.CordX, _startPositionTile.CordY);

        List<TileHelper> targetsPath = new List<TileHelper>();
        targetsPath = _map.GetPathToStart(_startPositionTile.CordX);
        TileHelper finish = targetsPath[targetsPath.Count - Offset];

        _map.AddObstacle(finish.CordX, finish.CordY);

        _moving = StartCoroutine(MoveToPath(targetsPath));
        _startPositionTile = finish;
    }

    private List<TileHelper> CreatePath(TileHelper start, TileHelper end)
    {
        _map.RemoveObstacle(start.CordX, start.CordY);
        _map.SetStart(end);
        _map.SetEnd(start);

        return _map.GetPath();
    }

    private void TryMoving(TileHelper start, TileHelper end)
    {
        _isMoving = true;

        List<TileHelper> hashPath = CreatePath(start, end);

        if (hashPath == null || IsAllTilePathCorrect(hashPath) == false)
        {
            if (_inParking == false)
            {
                MoveToZeroPosition();
            }
            else
            {
                _map.AddObstacle(start.CordX, start.CordY);
                _animator.WrongAnimationStart();
            }

            _isMoving = false;
            _isSelected = false;

            return;
        }

        if (_inParking)
        {
            _map.AddObstacle(_startPositionTile.CordX, _startPositionTile.CordY);
            _map.RemoveObstacle(_finishPositionTile.CordX, _finishPositionTile.CordY);
            _counter.RemoveScore(_finishPositionTile.Reward);
            _effector.PlayFinishNegativeEffects(transform.position);
            _finishPositionTile.SetDefaultState();
        }
        else
        {
            if (IsPositionEmpty(_finishPositionTile.CordX, _finishPositionTile.CordY))
            {
                _map.AddObstacle(_finishPositionTile.CordX, _finishPositionTile.CordY);
            }

            _map.RemoveObstacle(_startPositionTile.CordX, _startPositionTile.CordY);
        }

        OnMoving();
        _moving = StartCoroutine(MoveToPath(hashPath));
        hashPath = new List<TileHelper>();
    }

    private void OnMoving()
    {
        _effector.PlayMoveEffects(transformPipe);
        _viewer.DeactivateBackground();
        _outliner.enabled = false;
        _counter.RemoveStep();
    }

    private bool IsPositionEmpty(int x, int y)
    {
        return !_map.CheckObstacle(x, y);
    }

    private IEnumerator ChangingPositionOnQuenue()
    {
        bool isStartMove = true;
        _isMoving = true;

        while (_startPositionTile.CordY < _map.RoadOffVerticalValue)
        {
            if (IsPositionEmpty(_startPositionTile.CordX, _startPositionTile.CordY + Offset))
            {
                if (isStartMove == false)
                    _effector.PlayMoveQuenue();

                yield return StartCoroutine(Moving(_startPositionTile.CordY + Offset));
            }
            else
            {
                _map.AddObstacle(_startPositionTile.CordX, _startPositionTile.CordY);
                isStartMove = false;

                yield return new WaitForSeconds(UnityEngine.Random.Range(MinValueForChechObstacleOnQuenue, MaxValueForChechObstacleOnQuenue + Offset));
            }
        }

        _viewer.ActivateBackground();

        foreach (var item in _trailEffects)
        {
            item.DrawLine();
        }

        if (transform.position == _map.GetTile(_startPositionTile.CordX, _map.RoadOffVerticalValue).transform.position)
        {
            _map.AddObstacle(_startPositionTile.CordX, _startPositionTile.CordY);
            _outliner.enabled = true;
            _isMoving = false;
        }
    }

    private IEnumerator Moving(int coordY)
    {
        TileHelper tile = _map.GetTile(_startPositionTile.CordX, coordY);

        while (transform.position != tile.transform.position)
        {
            float step = _speed * Time.deltaTime * _movingQuenueMultiplicateValue;

            transform.position = Vector3.MoveTowards(transform.position, tile.transform.position, step);
            transform.LookAt(tile.transform);

            yield return null;
        }

        _map.RemoveObstacle(_startPositionTile.CordX, _startPositionTile.CordY);
        _startPositionTile = tile;
    }

    private IEnumerator MovingAway()
    {
        _moving = null;
        StopCoroutine(ChangingPositionOnQuenue());

        TileHelper tile = _map.GetTile(_startPositionTile.CordX, 0);

        while (transform.position != tile.transform.position)
        {
            float step = _speed * Time.deltaTime * _movingQuenueMultiplicateValue;

            transform.position = Vector3.MoveTowards(transform.position, tile.transform.position, step);
            transform.LookAt(tile.transform);

            yield return null;
        }
    }

    private bool IsAllTilePathCorrect(List<TileHelper> hashPath)
    {
        foreach (TileHelper item in hashPath)
            if (_map.CheckObstacle(item.CordX, item.CordY))
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

        if (_isSelected == false)
            _moving = null;

        if (transform.position.z >= MiddleTransform)
            _inParking = true;
        else
            _inParking = false;

        OnFinishPosition();
    }

    private void OnFinishPosition()
    {
        if (_inParking)
        {
            _map.AddObstacle(_finishPositionTile.CordX, _finishPositionTile.CordY);
            _counter.AddScore(_finishPositionTile.Reward);

            _effector.PlayFinishEffects(transform.position);
            _finishPositionTile.SetWinnerState();
        }
        else
        {
            if (_isEmptyFinisPlace)
            {
                _map.RemoveObstacle(_finishPositionTile.CordX, _finishPositionTile.CordY);
            }

            _viewer.ActivateBackground();
            transform.LookAt(_map.GetTile(_startPositionTile.CordX, _startPositionTile.CordY + (Offset + Offset)).transform);
            StartCoroutine(ChangingPositionOnQuenue());
        }

        _isSelected = false;
        _isMoving = false;
        _isEmptyFinisPlace = true;
    }
}
