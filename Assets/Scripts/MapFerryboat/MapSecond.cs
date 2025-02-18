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

    private void CreateItemOnMap()
    {
        for (int i = 0; i < 3; i++)
            _logic.AddVoid(6, i);

        for (int i = 0; i < Height; i++)
            _logic.AddVoid(0, i);

        for (int i = 1; i < 9; i++)
            _logic.AddVoid(4, i);

        for (int i = 4; i <= 6; i++)
            _logic.AddVoid(6, i);

        for (int i = 6; i < Height; i++)
            for (int j = 0; j < Width; j++)
                _logic.AddVoid(i, j);

        _logic.AddWall(1, 10, 2);
        _logic.AddWall(4, 10, 2);
        _logic.AddWall(5, 10, 2);

        for (int i = 1; i < 5; i++)
            for (int j = 11; j < Height; j++)
                _logic.AddCarFinishPoint(i, j);

        for (int i = 1; i <= 3; i++)
            for (int j = 0; j < _logic.CountFinishPlace / 3; j++)
                _logic.AddCarStartPoint(i, j);

        _logic.AddSpesialCarFinishPoint(5, 11);
        _logic.AddSpesialCarFinishPoint(5, 13);
        _logic.AddSpesialCarStartPoint(3, 1);
        _logic.AddSpesialCarStartPoint(2, 1);
    }
}