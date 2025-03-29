using UnityEngine;

public class MapSecond : Map
{
    private const int Width = 17;
    private const int Height = 17;

    [SerializeField] private MapLogic _logic;
    [SerializeField] private ObstacleLogic _obstaleLogic;

    public override void Activate()
    {
        _logic.Init(Width, Height);
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
        _logic.SetVaribleObstaclePlaces(Height - 1, 0, 4);
        _logic.SetVaribleObstaclePlaces(Height - 2, 1, 3);
    }

    private void CreateItemOnMap()
    {
        for (int i = 0; i < 3; i++)
            _logic.AddVoid(6, i);

        for (int i = 0; i < 10; i++)
            _logic.AddVoid(4, i);

        for (int i = 0; i < 10; i++)
            _logic.AddVoid(5, i);

        for (int i = 5; i < Height; i++)
            for (int j = 0; j < Width; j++)
                _logic.AddVoid(i, j);

        for (int i = 0; i < 4; i++)
            _obstaleLogic.SetBlockedFinishPlace(_logic.GetTile(i, Height - 1));

        _obstaleLogic.SetSpesialFinishPlace(_logic.GetTile(4, 16));
        _obstaleLogic.SetSpesialFinishPlace(_logic.GetTile(4, 14));

        for (int i = 0; i < 4; i++)
            for (int j = 11; j < Height; j++)
                _logic.AddCarFinishPoint(i, j);

        for (int i = 1; i < 4; i++)
            for (int j = 0; j < 3; j++)
                _logic.AddCarStartPoint(i, j);

        _logic.AddSpesialCarFinishPoint(4, 16);
        _logic.AddSpesialCarFinishPoint(4, 12);
        _logic.AddSpesialCarFinishPoint(4, 14);

        _logic.AddSpesialCarStartPoint(1, 2);
        _logic.AddSpesialCarStartPoint(3, 1);
        _logic.AddSpesialCarStartPoint(2, 1);

        _logic.AddWall(0, 10, 2);
        _logic.AddWall(1, 10, 2);
        _logic.AddWall(4, 10, 2);
    }
}