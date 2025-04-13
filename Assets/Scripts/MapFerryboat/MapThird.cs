﻿using UnityEngine;

public class MapThird : Map
{
    private const int Width = 24;
    private const int Height = 24;

    [SerializeField] private MapLogic _logic;
    [SerializeField] private ObstacleLogic _obstaleLogic;

    private int _roadOffVerticalValue;

    public override void Activate()
    {
        _roadOffVerticalValue = Width - 8;
        _logic.Init(Width, Height, _roadOffVerticalValue);
        gameObject.SetActive(true);
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
        _logic.SetVaribleObstaclePlaces(1, 5, _roadOffVerticalValue + 3, (Height - 2));
    }

    private void CreateItemOnMap()
    {
        for (int i = 0; i < Height - 7; i++)
        {
            _logic.AddVoid(4, i);
            _logic.AddVoid(5, i);
        }
            
        for (int i = 6; i < Height; i++)
            for (int j = 0; j < Width; j++)
                _logic.AddVoid(i, j);

        for (int i = 1; i < 5; i++)
            for (int j = Height - 6; j < Height; j++)
                _logic.AddCarFinishPoint(i, j);

        for (int j = 1; j < 2; j++)
            for (int i = 1; i < 4; i++)
                _logic.AddCarStartPoint(i, j);

        _logic.AddSpesialCarFinishPoint(5, Height - 2);
        _logic.AddSpesialCarFinishPoint(5, Height - 4);
        _logic.AddSpesialCarFinishPoint(5, Height - 6);
        _logic.AddSpesialCarFinishPoint(0, Height - 2);
        _logic.AddSpesialCarFinishPoint(0, Height - 4);
        _logic.AddSpesialCarFinishPoint(0, Height - 6);

        _logic.AddSpesialCarStartPoint(1, 1);
        _logic.AddSpesialCarStartPoint(3, 1);
        _logic.AddSpesialCarStartPoint(2, 1);
        _logic.AddSpesialCarStartPoint(3, 2);

        for (int i = 1; i < 5; i++)
            _obstaleLogic.SetBlockedFinishPlace(_logic.GetTile(i, Height - 1));

        _obstaleLogic.SetSpesialFinishPlace(_logic.GetTile(0, Height - 2));
        _obstaleLogic.SetSpesialFinishPlace(_logic.GetTile(0, Height - 4));
        _obstaleLogic.SetSpesialFinishPlace(_logic.GetTile(0, Height - 6));
        _obstaleLogic.SetSpesialFinishPlace(_logic.GetTile(5, Height - 2));
        _obstaleLogic.SetSpesialFinishPlace(_logic.GetTile(5, Height - 4));

        _logic.AddWall(0, Height - 7, 2);
        _logic.AddWall(3, Height - 7, 2);
        _logic.AddWall(4, Height - 7, 2);
        _logic.AddWall(5, Height - 7, 2);
        _logic.AddWall(6, Height - 7, 2);
    }
}
