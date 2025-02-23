using UnityEngine;

public class MapThird : Map
{
    private const int Width = 24;
    private const int Height = 24;

    [SerializeField] private MapLogic _logic;

    public override void Activate()
    {
        gameObject.SetActive(true);

        if (_logic.CountFinishPlace != 0)
            return;

        _logic.Init(Width, Height);
        CreateItemOnMap();
        SetObstacle();
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
        _logic.SetObstaclePlaces(Height - 1, 1, 4);
    }

    public override void CreateObstacle()
    {
        _logic.CreateObstacle();
    }

    public override void RemoveObstacle(TileHelper tile)
    {
        _logic.DeleteObstacle(tile);
    }

    private void CreateItemOnMap()
    {
        for (int i = 0; i < 3; i++)
            _logic.AddVoid(6, i);

        for (int i = 0; i < Height - 6; i++)
            _logic.AddVoid(5, i);

        for (int i = 1; i < 9; i++)
            _logic.AddVoid(4, i);

        for (int i = 4; i < 7; i++)
            _logic.AddVoid(6, i);

        for (int i = 7; i < Height; i++)
            for (int j = 0; j < Width; j++)
                _logic.AddVoid(i, j);

        _logic.AddWall(0, Height - 7, 2);
        _logic.AddWall(1, Height - 7, 2);
        _logic.AddWall(4, Height - 7, 2);
        _logic.AddWall(5, Height - 7, 2);
        _logic.AddWall(6, Height - 7, 2);

        for (int i = 1; i < 5; i++)
            for (int j = Height - 6; j < Height; j++)
                _logic.AddCarFinishPoint(i, j);

        for (int i = 1; i < 4; i++)
            for (int j = 0; j < _logic.CountFinishPlace / 3; j++)
                _logic.AddCarStartPoint(i, j);

        _logic.AddSpesialCarFinishPoint(6, Height - 2);
        _logic.AddSpesialCarFinishPoint(0, Height - 2);
        _logic.AddSpesialCarFinishPoint(0, Height - 4);
        _logic.AddSpesialCarFinishPoint(6, Height - 4);
        _logic.AddSpesialCarStartPoint(1, 1);
        _logic.AddSpesialCarStartPoint(3, 1);
        _logic.AddSpesialCarStartPoint(2, 1);
        _logic.AddSpesialCarStartPoint(3, 2);
    }
}
