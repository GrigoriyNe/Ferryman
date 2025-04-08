using System.Collections.Generic;
using UnityEngine;

public class MapFirst : Map
{
    private const int Width = 24;
    private const int Height = 24;
    private int _roadOffVerticalValue;

    [SerializeField] private MapLogic _logic;
    [SerializeField] private ObstacleLogic _obstaleLogic;

    public override void Activate()
    {
        _roadOffVerticalValue = Width - 8;
        gameObject.SetActive(true);
        _logic.Init(Width, Height, _roadOffVerticalValue);
        CreateItemOnMap();
        SetVaribleObstaclePlaces();
    }

    public override void Deactivate()
    {
        gameObject.SetActive(false);
        _logic.Deactivate();
    }

    public override int GetHeight()
    {
        return Height;
    }

    public override void SetVaribleObstaclePlaces()
    {
        _logic.SetVaribleObstaclePlaces(Height - 3, 0, 4);
        _logic.SetVaribleObstaclePlaces(Height - 4, 1, 3);
    }

    private void CreateItemOnMap()
    {
        for (int i = 4; i < 7; i++)
            _logic.AddVoid(6, i);

        for (int i = 0; i < Height; i++)
            for (int j = Height - 2; j < Height; j++)
                _logic.AddVoid(i, j);

        for (int i = 0; i < _roadOffVerticalValue + 1; i++)
            _logic.AddVoid(4, i);

        for (int i = 5; i < Height; i++)
            for (int j = 0; j < Width; j++)
                _logic.AddVoid(i, j);

        _obstaleLogic.SetSpesialFinishPlace(_logic.GetTile(4, Height - 5));
        _obstaleLogic.SetSpesialFinishPlace(_logic.GetTile(4, Height - 3));

        for (int i = 0; i < 4; i++)
            for (int j = Height - 6; j < Height - 2; j++)
                _logic.AddCarFinishPoint(i, j);

        for (int j = 1; j < 2; j++)
            for (int i = 1; i < 4; i++)
                _logic.AddCarStartPoint(i, j);

        _logic.AddSpesialCarFinishPoint(4, Height - 5);
        _logic.AddSpesialCarFinishPoint(4, Height - 3);
        _logic.AddSpesialCarStartPoint(3, 1);
        _logic.AddSpesialCarStartPoint(2, 1);

        _logic.AddWall(0, _roadOffVerticalValue + 1, 2);
        _logic.AddWall(3, _roadOffVerticalValue + 1, 2);
        _logic.AddWall(4, _roadOffVerticalValue + 1, 2);
    }
}
