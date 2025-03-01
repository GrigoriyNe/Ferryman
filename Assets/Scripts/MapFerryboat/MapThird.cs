using UnityEngine;

public class MapThird : Map
{
    private const int Width = 24;
    private const int Height = 24;

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
        _logic.SetVaribleObstaclePlaces(Height - 1, 1, 4);
    }

    private void CreateItemOnMap()
    {
        for (int i = 0; i < Height - 6; i++)
        {
            _logic.AddVoid(4, i);
            _logic.AddVoid(5, i);
            _logic.AddVoid(6, i);
        }
            
        for (int i = 7; i < Height; i++)
            for (int j = 0; j < Width; j++)
                _logic.AddVoid(i, j);

        _logic.AddWall(0, Height - 7, 2);
        _logic.AddWall(4, Height - 6, 0);
        _logic.AddWall(5, Height - 6, 0);
        _logic.AddWall(6, Height - 6, 0);
        _logic.AddWall(7, Height - 6, 0);

        

        for (int i = 1; i < 6; i++)
            for (int j = Height - 6; j < Height; j++)
                _logic.AddCarFinishPoint(i, j);

        for (int i = 1; i < 4; i++)
            for (int j = 0; j < 5; j++)
                _logic.AddCarStartPoint(i, j);

        _logic.AddSpesialCarFinishPoint(6, Height - 2);
        _logic.AddSpesialCarFinishPoint(6, Height - 4);
        _logic.AddSpesialCarFinishPoint(6, Height - 6);
        _logic.AddSpesialCarFinishPoint(0, Height - 2);
        _logic.AddSpesialCarFinishPoint(0, Height - 4);
        _logic.AddSpesialCarFinishPoint(0, Height - 6);

        _logic.AddSpesialCarStartPoint(1, 1);
        _logic.AddSpesialCarStartPoint(3, 1);
        _logic.AddSpesialCarStartPoint(2, 1);
        _logic.AddSpesialCarStartPoint(3, 2);

        for (int i = 1; i < 6; i++)
            _obstaleLogic.SetBlockedStarPlace(_logic.GetTile(i, Height - 1));

        _obstaleLogic.SetSpesialBlockedStarPlace(_logic.GetTile(0, Height - 2));
        _obstaleLogic.SetSpesialBlockedStarPlace(_logic.GetTile(0, Height - 4));
        _obstaleLogic.SetSpesialBlockedStarPlace(_logic.GetTile(0, Height - 6));
        _obstaleLogic.SetSpesialBlockedStarPlace(_logic.GetTile(6, Height - 6));
    }
}
