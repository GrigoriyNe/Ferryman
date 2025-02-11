using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour //, IMoveable
{
    [SerializeField] private float _speed;
    [SerializeField] private MoverLogic moverLogic;

    private TileHelper _startPositionTile;
    private TileHelper _finishPositionTile;

    private Coroutine _moving;
    private bool _isMoving;

    private void OnEnable()
    {
        
        _moving = null;
        _isMoving = false;
        
    }

    public void Init(TileHelper startPositionTile, TileHelper finishPositionTile)
    {
        _startPositionTile = startPositionTile;
        _finishPositionTile = finishPositionTile;
        transform.position = _startPositionTile.transform.position;
        Move();
    }

    public void Move()
    {
        moverLogic.SetStart(_finishPositionTile);
        moverLogic.SetEnd(_startPositionTile);
        StartCoroutine(MoveTo(moverLogic.GetPath()));
        _isMoving = true;
        // targets));
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

            if (transform.position == targets[i].transform.position)
                i++;

            //     CheckPosition();
        }
    }

    //private void CheckPosition()
    //{
    //    if (transform.position == _targets)
    //    {
    //   //     StopCoroutine(_moving);
    //        _isMoving = false;
    //    }
    //}
}
