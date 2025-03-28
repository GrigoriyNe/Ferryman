﻿using System.Collections.Generic;
using UnityEngine;

public class MapFirst : Map
{
    private const int Width = 15;
    private const int Height = 15;

    [SerializeField] private MapLogic _logic;
    [SerializeField] private ObstacleLogic _obstaleLogic;

    public override void Activate()
    {
        gameObject.SetActive(true);
        _logic.Init(Width, Height);
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
        _logic.SetVaribleObstaclePlaces(Height - 1, 0, 4);
        _logic.SetVaribleObstaclePlaces(Height - 2, 1, 3);
    }

    private void CreateItemOnMap()
    {
        for (int i = 4; i < 7; i++)
            _logic.AddVoid(6, i);

        for (int i = 0; i < 10; i++)
            _logic.AddVoid(4, i);

        for (int i = 5; i < Height; i++)
            for (int j = 0; j < Width; j++)
                _logic.AddVoid(i, j);

        _obstaleLogic.SetSpesialFinishPlace(_logic.GetTile(4, 12));
        _obstaleLogic.SetSpesialFinishPlace(_logic.GetTile(4, Height - 1));
        _obstaleLogic.SetSpesialFinishPlace(_logic.GetTile(4, Height - 2));

        for (int i = 0; i < 4; i++)
            for (int j = 11; j < Height; j++)
                _logic.AddCarFinishPoint(i, j);

        for (int i = 1; i < 4; i++)
            for (int j = 0; j < 3; j++)
                _logic.AddCarStartPoint(i, j);

        _logic.AddSpesialCarFinishPoint(4, 12);
        _logic.AddSpesialCarFinishPoint(4, Height - 1);
        _logic.AddSpesialCarFinishPoint(4, Height - 2);
        _logic.AddSpesialCarStartPoint(3, 1);

        _logic.AddWall(0, 10, 2);
        _logic.AddWall(3, 10, 2);
        _logic.AddWall(4, 10, 2);
        _logic.AddWall(5, 10, 2);

    }
}
