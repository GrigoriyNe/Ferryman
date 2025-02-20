using UnityEngine;

public class MapFirst : Map
{
    private const int Width = 15;
    private const int Height = 15;

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

    private void CreateItemOnMap()
    {
        for (int i = 4; i < 7; i++)
            _logic.AddVoid(6, i);

        for (int i = 0; i < 11; i++)
            _logic.AddVoid(4, i);

        for (int i = 5; i < Height; i++)
            for (int j = 0; j < Width; j++)
                _logic.AddVoid(i, j);

        _logic.AddWall(0, 10, 2);
        _logic.AddWall(3, 10, 2);
        _logic.AddWall(4, 10, 2);

        for (int i = 0; i < 4; i++)
            for (int j = 11; j < Height; j++)
                _logic.AddCarFinishPoint(i, j);

        for (int i = 1; i <4; i++)
            for (int j = 0; j < _logic.CountFinishPlace / 3; j++)
                _logic.AddCarStartPoint(i, j);

        _logic.AddSpesialCarFinishPoint(4, 12);
        _logic.AddSpesialCarStartPoint(3, 1);
    }
}
