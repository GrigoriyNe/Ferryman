using UnityEngine;

public class MapSecond : Map
{
    private const int Width = 24;
    private const int Height = 24;

    private const int RoadOffVerticalValue = Width - MiddleOffset;
    private const int MiddleOffset = 8;
    private const int LowestThresholdX = 1;
    private const int BigestThresholdX = 4;
    private const int RoadOffVerticalLowerValue = 3;

    private const int HeightOffset = 2;
    private const int VerticalRigthBorder = 4;
    private const int VerticalOffset = 1;

    private const int HorizontalFirstSpesialPlase = Height - 2;
    private const int HorizontalSecondSpesialPlase = Height - 4;
    private const int HorizontalThirdSpesialPlase = Height - 6;

    private const int HorizontalParkLower = RoadOffVerticalValue + 2;
    private const int HorizontalParkHeigth = Height -1;

    private const int StartVertcalPlase = 3;

    private const int WallHorizontalPlace = RoadOffVerticalValue + 1;
    private const int WallState = 2;
    private const int WallVerticalFirst = 0;
    private const int WallVerticalSecond = 3;
    private const int WallVerticalThird = 4;

    [SerializeField] private MapLogic _logic;
    [SerializeField] private ObstacleLogic _obstaleLogic;

    public override void Activate()
    {
        _logic.Init(Width, Height, RoadOffVerticalValue);
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
        _logic.SetVaribleObstaclePlaces(LowestThresholdX, BigestThresholdX,
            RoadOffVerticalValue + RoadOffVerticalLowerValue, (Height - RoadOffVerticalLowerValue));
    }

    private void CreateItemOnMap()
    {
        for (int i = 0; i < RoadOffVerticalValue + VerticalOffset; i++)
            _logic.AddVoid(VerticalRigthBorder, i);

        for (int i = 0; i < RoadOffVerticalValue; i++)
            _logic.AddVoid(VerticalRigthBorder + VerticalOffset, i);

        for (int i = VerticalRigthBorder + VerticalOffset; i < Height; i++)
            for (int j = 0; j < Width; j++)
                _logic.AddVoid(i, j);

        for (int i = 0; i < Height; i++)
            for (int j = Height - VerticalOffset; j < Height; j++)
                _logic.AddVoid(i, j);

        for (int i = 0; i < VerticalRigthBorder; i++)
            _obstaleLogic.SetBlockedFinishPlace(_logic.GetTile(i, Height - HeightOffset));

        _obstaleLogic.SetSpesialFinishPlace(_logic.GetTile(VerticalRigthBorder, HorizontalFirstSpesialPlase));
        _obstaleLogic.SetSpesialFinishPlace(_logic.GetTile(VerticalRigthBorder, HorizontalSecondSpesialPlase));

        for (int i = 0; i < VerticalRigthBorder; i++)
            for (int j = HorizontalParkLower; j < HorizontalParkHeigth; j++)
                _logic.AddCarFinishPoint(i, j);

        for (int i = VerticalOffset; i < VerticalRigthBorder; i++)
        {
            _logic.AddCarStartPoint(i, StartVertcalPlase);
            _logic.AddSpesialCarStartPoint(i, StartVertcalPlase);
        };

        _logic.AddSpesialCarFinishPoint(VerticalRigthBorder, HorizontalFirstSpesialPlase);
        _logic.AddSpesialCarFinishPoint(VerticalRigthBorder, HorizontalSecondSpesialPlase);
        _logic.AddSpesialCarFinishPoint(VerticalRigthBorder, HorizontalThirdSpesialPlase);

        _logic.AddWall(WallVerticalFirst, WallHorizontalPlace, WallState);
        _logic.AddWall(WallVerticalSecond, WallHorizontalPlace, WallState);
        _logic.AddWall(WallVerticalThird, WallHorizontalPlace, WallState);
    }
}