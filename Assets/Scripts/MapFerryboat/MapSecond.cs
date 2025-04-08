using UnityEngine;

public class MapSecond : Map
{
    private const int Width = 24;
    private const int Height = 24;
    private int _roadOffVerticalValue;

    [SerializeField] private MapLogic _logic;
    [SerializeField] private ObstacleLogic _obstaleLogic;

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
        _logic.SetVaribleObstaclePlaces(Height - 4, 0, 3);  
        _logic.SetVaribleObstaclePlaces(Height - 3, 1, 3);
    }

    private void CreateItemOnMap()
    {
        for (int i = 0; i < 3; i++)
            _logic.AddVoid(6, i);

        for (int i = 0; i < _roadOffVerticalValue + 1; i++)
            _logic.AddVoid(4, i);

        for (int i = 0; i < 10; i++)
            _logic.AddVoid(5, i);

        for (int i = 5; i < Height; i++)
            for (int j = 0; j < Width; j++)
                _logic.AddVoid(i, j);

        for (int i = 0; i < Height; i++)
            for (int j = Height -1; j < Width; j++)
                _logic.AddVoid(i, j);

        for (int i = 0; i < 4; i++)
            _obstaleLogic.SetBlockedFinishPlace(_logic.GetTile(i, Height - 2));

        _obstaleLogic.SetSpesialFinishPlace(_logic.GetTile(4, Height - 3));
        _obstaleLogic.SetSpesialFinishPlace(_logic.GetTile(4, Height - 4));

        for (int i = 0; i < 4; i++)
            for (int j = _roadOffVerticalValue + 2; j < Height - 1; j++)
                _logic.AddCarFinishPoint(i, j);

        for (int j = 1; j < 2; j++)
            for (int i = 1; i < 4; i++)
                _logic.AddCarStartPoint(i, j);

        _logic.AddSpesialCarFinishPoint(4, Height - 3);
        _logic.AddSpesialCarFinishPoint(4, Height - 4);
        _logic.AddSpesialCarFinishPoint(4, Height - 5);

        _logic.AddSpesialCarStartPoint(1, 2);
        _logic.AddSpesialCarStartPoint(3, 1);
        _logic.AddSpesialCarStartPoint(2, 1);

        _logic.AddWall(0, _roadOffVerticalValue + 1, 2);
        _logic.AddWall(3, _roadOffVerticalValue + 1, 2);
        _logic.AddWall(4, _roadOffVerticalValue + 1, 2);
    }
}