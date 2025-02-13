using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Car : MonoBehaviour //, IMoveable
{
    [SerializeField] private float _speed;
    [SerializeField] private MoverLogic _moverLogic;
    [SerializeField] private TextMeshProUGUI _viewFinishPosition;

    private TileHelper _startPositionTile;
    private TileHelper _finishPositionTile;

    private bool _isMoving;
    private bool _onParking;
    private bool _isActive;

    private Coroutine _moving = null;

    private void OnEnable()
    {
        _isMoving = false;
        _onParking = false;
        _isActive = false;
    }

    public void Init(TileHelper startPositionTile, TileHelper finishPositionTile)
    {
        _startPositionTile = startPositionTile;
        _finishPositionTile = finishPositionTile;
        transform.position = _startPositionTile.transform.position;
        _viewFinishPosition.text = GetTextPosition();

        CheckNextPosition();
    }

    private void CheckNextPosition()
    {
        if (_moverLogic.CheckObstacle(_startPositionTile.cord_x, _startPositionTile.cord_y + 1) == false)
            _isActive = true;
    }

    private string GetTextPosition()
    {
        string TextPositionX = null;

        if (_finishPositionTile.cord_x == 0)
            TextPositionX = "A";
        if (_finishPositionTile.cord_x == 1)
            TextPositionX = "B";
        if (_finishPositionTile.cord_x == 2)
            TextPositionX = "C";
        if (_finishPositionTile.cord_x == 3)
            TextPositionX = "D";
        if (_finishPositionTile.cord_x == 4)
            TextPositionX = "E";
        if (_finishPositionTile.cord_x == 5)
            TextPositionX = "F";

        return TextPositionX + (1 + _finishPositionTile.cord_y).ToString();
    }

    public void Move()
    {
        if (_isMoving)
            return;

        if (_isActive == false)
            return;

        if (_onParking == false)
        {
            TryMoving(_startPositionTile, _finishPositionTile);

        }
        else
        {
            TryMoving(_finishPositionTile, _startPositionTile);
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

        _moving = StartCoroutine(MoveTo(_moverLogic.GetPath()));
    }

    private IEnumerator MoveTo(List<TileHelper> targets)
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
        _onParking = !_onParking;
        SetPosition();
    }


    private void SetPosition()
    {
        if (_onParking)
        {
            transform.position = _finishPositionTile.transform.position;
            _moverLogic.AddObstacle(_finishPositionTile.cord_x, _finishPositionTile.cord_y);
        }
        else
        {
            _moverLogic.AddObstacle(_startPositionTile.cord_x, _startPositionTile.cord_y);
            transform.position = _startPositionTile.transform.position;
        }
    }
}
