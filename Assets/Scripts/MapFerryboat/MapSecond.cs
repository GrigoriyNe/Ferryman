using UnityEngine;

public class MapSecond : Map
{
    private const int Width = 17;
    private const int Height = 17;

    [SerializeField] private MapLogic _logic;

    public override void Activate()
    {
        gameObject.SetActive(true);
        _logic.Init(Width, Height);
        CreateItemOnMap();
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
    public override void SetObstacle()
    {
        _logic.SetObstaclePlaces(Height - 1, 0, 4);
    }

    public override void CreateObstacle()
    {
        _logic.CreateObstacle();
    }

    public override void RemoveObstacle()
    {
        _logic.DeleteObstacle();
    }

    private void CreateItemOnMap()
    {
        for (int i = 0; i < 3; i++)
            _logic.AddVoid(6, i);

        for (int i = 0; i < 11; i++)
            _logic.AddVoid(5, i);

        for (int i = 0; i <11; i++)
            _logic.AddVoid(6, i);

        for (int i = 7; i < Height; i++)
            for (int j = 0; j < Width; j++)
                _logic.AddVoid(i, j);

        _logic.AddWall(0, 11, 2);
        _logic.AddWall(1, 11, 2);
        _logic.AddWall(4, 11, 2);
        _logic.AddWall(5, 11, 2);
        _logic.AddWall(6, 11, 2);

        for (int i = 1; i < 5; i++)
            for (int j = 12; j < Height; j++)
                _logic.AddCarFinishPoint(i, j);

        for (int i = 2; i <5; i++)
            for (int j = 0; j < _logic.CountFinishPlace / 3; j++)
                _logic.AddCarStartPoint(i, j);

        _logic.AddSpesialCarFinishPoint(5, 12);
        _logic.AddSpesialCarFinishPoint(5, 14);
        _logic.AddSpesialCarStartPoint(3, 1);
        _logic.AddSpesialCarStartPoint(2, 1);
    }
}