using UnityEngine;

public class Car : SpawnableObject
{
    [SerializeField] private CarMover _mover;
    [SerializeField] private CarTextViewer _textViewer;

    public void Init(TileHelper startPositionTile, TileHelper finishPositionTile, Namer parkPlace)
    {
        _mover.Init(startPositionTile, finishPositionTile, parkPlace);
        _textViewer.Init(parkPlace, finishPositionTile);
        transform.position = startPositionTile.transform.position;
        
        _mover.MoveInQuenue();
    }

    public void Move()
    {
        _mover.Move();
    }

    public void MoveAway()
    {
        _mover.MoveAway();
    }
}
