using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour //, IMoveable
{
    [SerializeField] private float _speed;
    [SerializeField] private MoverLogic _moverLogic;

    private TileHelper _startPositionTile;
    private TileHelper _finishPositionTile;

    private bool _isMoving;
    private bool _onParking;

    private Coroutine _moving = null;

    private void OnEnable()
    {
        _isMoving = false;
        _onParking = false;
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.collider.TryGetComponent(out Car _))
    //    {
    //        _moving = null;
    //        MoveToStart();
    //        _onParking=true;
    //    }
    //}

    public void Init(TileHelper startPositionTile, TileHelper finishPositionTile)
    {
        _startPositionTile = startPositionTile;
        _finishPositionTile = finishPositionTile;
        transform.position = _startPositionTile.transform.position;
    }

    public void Move()
    {
        if (_isMoving)
            return;

        if (_onParking == false)
        {
        //    _isMoving = true;

            //_moverLogic.RemoveObstacle(_startPositionTile.cord_x, _startPositionTile.cord_y);

            //_moverLogic.SetStart(_finishPositionTile);
            //_moverLogic.SetEnd(_startPositionTile);
            TryMoving(_startPositionTile, _finishPositionTile);
        }
        else
        {
            //_isMoving = true;
            //_moverLogic.RemoveObstacle(_finishPositionTile.cord_x, _finishPositionTile.cord_y);
            //MoveToStart();

            TryMoving(_finishPositionTile, _startPositionTile);
        }
    }

    //public void MoveToStart()
    //{

    //    //_moverLogic.SetEnd(_startPositionTile);
    //    //_moverLogic.SetStart(_finishPositionTile);
    //    TryMoving(_startPositionTile, _finishPositionTile);
    //}

    private void TryMoving(TileHelper start, TileHelper end)
    {
        _isMoving = true;

        _moverLogic.RemoveObstacle(start.cord_x, start.cord_y);
        _moverLogic.SetStart(end);
        _moverLogic.SetEnd(start);

        if (_moverLogic.GetPath() == null)
            return;


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
