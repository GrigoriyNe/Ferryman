using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CarAnimator))]
public class CarMover : MonoBehaviour
{
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
    private WaitForSeconds _wait15Millisecond;
    private float _delay15Millisecond = 1.5f;
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

    private void SetWaitings()
    {
        _wait1Millisecond = new WaitForSeconds(_delay1Millisecond);
        _wait15Millisecond = new WaitForSeconds(_delay15Millisecond);
    }

    public void MoveInQuenue()
    {
        StartCoroutine(ChangingPositionOnQuenue());
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

        if (_counter.ScoreLess <= 1)
            return;

        _isSelected = true;

        if (_inParking)
        {
            _startPositionTile = _map.GetFreePlaceOnQenue(_startPositionTile.cordX);
            _effector.PlayFinishNegativeEffects(transform.position);
            _finishPositionTile.SetDefaultState();

            TryMoving(_finishPositionTile, _startPositionTile);
        }
        else
        {
            if (IsPositionEmpty(_startPositionTile.cordX, _startPositionTile.cordY + 1) == false)
            {
                _animator.WrongAnimationStart();

                return;
            }

            if (IsPositionEmpty(_finishPositionTile.cordX, _finishPositionTile.cordY) == false)
            {
                _isEmptyFinisPlace = false;
                _animator.TurnAnimationStart();

                TileHelper target = _map.GetTile(_startPositionTile.cordX, 0);

                if (IsPositionEmpty(_startPositionTile.cordX, _startPositionTile.cordY - 1)
                    && IsPositionEmpty(_startPositionTile.cordX, _startPositionTile.cordY - 2))
                {
                    _map.AddObstacle(_startPositionTile.cordX, _startPositionTile.cordY - 1);

                    TryMoving(_startPositionTile, target);

                    if (_map.gameObject.activeSelf)
                        _map.RemoveObstacle(_startPositionTile.cordX, _startPositionTile.cordY - 1);

                    _startPositionTile = target;

                    return;
                }


                TryMoving(_startPositionTile, target);

                _startPositionTile = target;

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

    private bool IsPositionEmpty(int x, int y)
    {
        return !_map.CheckObstacle(x, y);
    }

    private IEnumerator ChangingPositionOnQuenue()
    {
        bool isStartMove = true;
        _isMoving = true;

        while (_startPositionTile.cordY < _map.RoadOffVerticalValue)
        {
            if (IsPositionEmpty(_startPositionTile.cordX, _startPositionTile.cordY + 1))
            {
                if (isStartMove == false)
                    _effector.PlayMoveQuenue();

                _map.AddObstacle(_startPositionTile.cordX, _startPositionTile.cordY);

                yield return StartCoroutine(Moving(_startPositionTile.cordY + 1));
            }
            else
            {
                _map.AddObstacle(_startPositionTile.cordX, _startPositionTile.cordY);
                isStartMove = false;

                yield return new WaitForSeconds(UnityEngine.Random.Range(0.2f, 2f));
            }
        }

        _viewer.ActivateBackground();

        foreach (var item in _trailEffects)
        {
            item.DrawLine();
        }

        if (transform.position == _map.GetTile(_startPositionTile.cordX, _map.RoadOffVerticalValue).transform.position)
        {
            _outliner.enabled = true;

            _isMoving = false;
        }
    }

    private IEnumerator Moving(int coordY)
    {
        TileHelper tile = _map.GetTile(_startPositionTile.cordX, coordY);

        while (transform.position != tile.transform.position)
        {
            float step = _speed * Time.deltaTime * _movingQuenueMultiplicateValue;

            transform.position = Vector3.MoveTowards(transform.position, tile.transform.position, step);
            transform.LookAt(tile.transform);

            yield return null;
        }

        _startPositionTile = tile;
        _map.AddObstacle(_startPositionTile.cordX, _startPositionTile.cordY);
        _map.RemoveObstacle(_startPositionTile.cordX, _startPositionTile.cordY - 1);
    }

    private IEnumerator MovingAway()
    {
        _moving = null;
        StopCoroutine(ChangingPositionOnQuenue());

        TileHelper tile = _map.GetTile(_startPositionTile.cordX, 0);

        while (transform.position != tile.transform.position)
        {
            float step = _speed * Time.deltaTime * _movingQuenueMultiplicateValue;

            transform.position = Vector3.MoveTowards(transform.position, tile.transform.position, step);
            transform.LookAt(tile.transform);

            yield return null;
        }
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
            _isSelected = false;

            return;
        }

        _outliner.enabled = false;

        if (_inParking)
        {
            _map.AddObstacle(_startPositionTile.cordX, _startPositionTile.cordY);
            _counter.RemoveScore(_finishPositionTile.Reward);
        }
        else
        {
            if (IsPositionEmpty(_finishPositionTile.cordX, _finishPositionTile.cordY))
            {
                _map.AddObstacle(_finishPositionTile.cordX, _finishPositionTile.cordY);
            }
        }

        _effector.PlayMoveEffects(transformPipe);
        _viewer.DeactivateBackground();



        _counter.RemoveStep();
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

        if (_isSelected == false)
            _moving = null;

        if (transform.position.z >= 10)
            _inParking = true;
        else
            _inParking = false;

        OnFinishPosition();
    }

    private void OnFinishPosition()
    {
        if (_inParking)
        {
            _map.AddObstacle(_finishPositionTile.cordX, _finishPositionTile.cordY);
            _counter.AddScore(_finishPositionTile.Reward);

            _effector.PlayFinishEffects(transform.position);
            _finishPositionTile.SetWinnerState();
        }
        else
        {
            if (_isEmptyFinisPlace)
            {
                _map.RemoveObstacle(_finishPositionTile.cordX, _finishPositionTile.cordY);
            }

            _viewer.ActivateBackground();
            transform.LookAt(_map.GetTile(_startPositionTile.cordX, _startPositionTile.cordY + 2).transform);
            StartCoroutine(ChangingPositionOnQuenue());
        }

        _isSelected = false;
        _isMoving = false;
        _isEmptyFinisPlace = true;
    }
}
