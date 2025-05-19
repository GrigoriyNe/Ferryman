using UnityEngine;

namespace MapFerryboat
{
    public class SecondLevel : Map
    {
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
        private const int HorizontalParkHeigth = Height - 1;

        private const int StartVertcalPlase = 3;

        private const int WallHorizontalPlace = RoadOffVerticalValue + 1;
        private const int WallState = 2;
        private const int WallVerticalFirst = 0;
        private const int WallVerticalSecond = 3;
        private const int WallVerticalThird = 4;

        [SerializeField] private Obstacle.ObstacleLogic _obstaleLogic;

        public override void SetVaribleObstaclePlaces()
        {
            Logic.SetVaribleObstaclePlaces(LowestThresholdX, BigestThresholdX,
                RoadOffVerticalValue + RoadOffVerticalLowerValue, Height - RoadOffVerticalLowerValue);
        }

        public override void CreateItemOnMap()
        {
            for (int i = 0; i < RoadOffVerticalValue + VerticalOffset; i++)
                Logic.AddVoid(VerticalRigthBorder, i);

            for (int i = 0; i < RoadOffVerticalValue; i++)
                Logic.AddVoid(VerticalRigthBorder + VerticalOffset, i);

            for (int i = VerticalRigthBorder + VerticalOffset; i < Height; i++)
                for (int j = 0; j < Height; j++)
                    Logic.AddVoid(i, j);

            for (int i = 0; i < Height; i++)
                for (int j = Height - VerticalOffset; j < Height; j++)
                    Logic.AddVoid(i, j);

            for (int i = 0; i < VerticalRigthBorder; i++)
                _obstaleLogic.SetBlockedFinishPlace(Logic.GetTile(i, Height - HeightOffset));

            _obstaleLogic.SetSpesialFinishPlace(Logic.GetTile(VerticalRigthBorder, HorizontalFirstSpesialPlase));
            _obstaleLogic.SetSpesialFinishPlace(Logic.GetTile(VerticalRigthBorder, HorizontalSecondSpesialPlase));

            for (int i = 0; i < VerticalRigthBorder; i++)
                for (int j = HorizontalParkLower; j < HorizontalParkHeigth; j++)
                    Logic.AddCarFinishPoint(i, j);

            for (int i = VerticalOffset; i < VerticalRigthBorder; i++)
            {
                Logic.AddCarStartPoint(i, StartVertcalPlase);
                Logic.AddSpesialCarStartPoint(i, StartVertcalPlase);
            }

            Logic.AddSpesialCarFinishPoint(VerticalRigthBorder, HorizontalFirstSpesialPlase);
            Logic.AddSpesialCarFinishPoint(VerticalRigthBorder, HorizontalSecondSpesialPlase);
            Logic.AddSpesialCarFinishPoint(VerticalRigthBorder, HorizontalThirdSpesialPlase);

            Logic.AddWall(WallVerticalFirst, WallHorizontalPlace, WallState);
            Logic.AddWall(WallVerticalSecond, WallHorizontalPlace, WallState);
            Logic.AddWall(WallVerticalThird, WallHorizontalPlace, WallState);
        }
    }
}