using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Car : MonoBehaviour //, IMoveable
{
    [SerializeField] private float _speed;
    [SerializeField] private MoverLogic _moverLogic;
    [SerializeField] private TextMeshProUGUI _viewFinishPosition;
    [SerializeField] private Image _backgroundText;
    [SerializeField] private NameParkingPlaces _placesName;

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
            if (CheckNextPosition())
            {
                _isMoving = true;

                _moverLogic.RemoveObstacle(_startPositionTile.cord_x, _startPositionTile.cord_y);
                transform.position = _moverLogic.GetTile(_startPositionTile.cord_x, _startPositionTile.cord_y + 1).transform.position;
                _startPositionTile = _moverLogic.GetTile(_startPositionTile.cord_x, _startPositionTile.cord_y + 1);
                _moverLogic.AddObstacle(_startPositionTile.cord_x, _startPositionTile.cord_y);
            }

            yield return new WaitForSeconds(0.666f);
        }

        _isMoving = false;
    }

    private bool CheckNextPosition()
    {
        return !_moverLogic.CheckObstacle(_startPositionTile.cord_x, _startPositionTile.cord_y + 1);
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
        _backgroundText.gameObject.SetActive(false);

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

        _moverLogic.RemoveObstacle(start.cord_x, start.cord_y);
        _moverLogic.SetStart(end);
        _moverLogic.SetEnd(start);

        if (_moverLogic.GetPath() == null)
        {
            _moverLogic.AddObstacle(start.cord_x, start.cord_y);
            _isMoving = false;
            return;
        }

        _moving = StartCoroutine(MoveToPath(_moverLogic.GetPath()));
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
            _startPositionTile = _moverLogic.GetTile(_startPositionTile.cord_x, 0);
            _inParking = !_inParking;
        }

        _isSelected = false;

        if (_inParking)
        {
            transform.position = _finishPositionTile.transform.position;
            _moverLogic.AddObstacle(_finishPositionTile.cord_x, _finishPositionTile.cord_y);
        }
        else
        {
            StartCoroutine(MovingInQuenue());
            _moverLogic.AddObstacle(_startPositionTile.cord_x, _startPositionTile.cord_y);
            transform.position = _startPositionTile.transform.position;
            transform.LookAt(_moverLogic.GetTile(_startPositionTile.cord_x, _startPositionTile.cord_y + 1).transform);
            _backgroundText.gameObject.SetActive(true);
        }
    }
}
