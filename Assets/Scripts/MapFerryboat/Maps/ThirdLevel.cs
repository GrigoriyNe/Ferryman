using UnityEngine;

namespace MapFerryboat
{
    public class ThirdLevel : Map
    {
        private const int LowestThresholdX = 1;
        private const int VaribleObsaleHorizontalLower = RoadOffVerticalValue + 3;
        private const int VaribleObsaleHorizontalHeigth = Height - 2;

        private const int RoadOffVerticalLowerValue = Height - 7;

        private const int HeightOffset = 1;
        private const int VerticalRigthBorder = 5;
        private const int VerticalLeftBorder = 0;
        private const int VerticalOffset = 1;

        private const int HorizontalFirstSpesialPlase = Height - 2;
        private const int HorizontalSecondSpesialPlase = Height - 4;
        private const int HorizontalThirdSpesialPlase = Height - 6;

        private const int StartVertcalPlase = 3;
        private const int RigthStartPole = 3;

        private const int WallHorizontalPlace = Height - 7;
        private const int WallState = 2;
        private const int WallVerticalFirst = 0;
        private const int WallVerticalSecond = 3;
        private const int WallVerticalThird = 4;
        private const int WallVerticalFive = 5;
        private const int WallVerticalSix = 6;

        [SerializeField] private Obstacle.ObstacleLogic _obstaleLogic;

        public override void SetVaribleObstaclePlaces()
        {
            Logic.SetVaribleObstaclePlaces(LowestThresholdX, VerticalRigthBorder,
                VaribleObsaleHorizontalLower, VaribleObsaleHorizontalHeigth);
        }

        public override void CreateItemOnMap()
        {
            for (int i = 0; i < RoadOffVerticalLowerValue; i++)
            {
                Logic.AddVoid(RigthStartPole + VerticalOffset, i);
                Logic.AddVoid(RigthStartPole + VerticalOffset + VerticalOffset, i);
            }

            for (int i = VerticalRigthBorder + VerticalOffset; i < Height; i++)
                for (int j = 0; j < Height; j++)
                    Logic.AddVoid(i, j);

            for (int i = VerticalOffset; i < VerticalRigthBorder; i++)
                for (int j = RoadOffVerticalLowerValue + VerticalOffset; j < Height; j++)
                    Logic.AddCarFinishPoint(i, j);

            for (int i = VerticalOffset; i < VerticalRigthBorder - VerticalOffset; i++)
            {
                Logic.AddCarStartPoint(i, StartVertcalPlase);
                Logic.AddSpesialCarStartPoint(i, StartVertcalPlase);
            }

            Logic.AddSpesialCarFinishPoint(VerticalRigthBorder, HorizontalFirstSpesialPlase);
            Logic.AddSpesialCarFinishPoint(VerticalRigthBorder, HorizontalSecondSpesialPlase);
            Logic.AddSpesialCarFinishPoint(VerticalRigthBorder, HorizontalThirdSpesialPlase);
            Logic.AddSpesialCarFinishPoint(VerticalLeftBorder, HorizontalFirstSpesialPlase);
            Logic.AddSpesialCarFinishPoint(VerticalLeftBorder, HorizontalSecondSpesialPlase);
            Logic.AddSpesialCarFinishPoint(VerticalLeftBorder, HorizontalThirdSpesialPlase);

            for (int i = VerticalOffset; i < VerticalRigthBorder; i++)
                _obstaleLogic.SetBlockedFinishPlace(Logic.GetTile(i, Height - HeightOffset));

            _obstaleLogic.SetSpesialFinishPlace(Logic.GetTile(VerticalLeftBorder, HorizontalFirstSpesialPlase));
            _obstaleLogic.SetSpesialFinishPlace(Logic.GetTile(VerticalLeftBorder, HorizontalSecondSpesialPlase));
            _obstaleLogic.SetSpesialFinishPlace(Logic.GetTile(VerticalLeftBorder, HorizontalThirdSpesialPlase));
            _obstaleLogic.SetSpesialFinishPlace(Logic.GetTile(VerticalRigthBorder, HorizontalFirstSpesialPlase));
            _obstaleLogic.SetSpesialFinishPlace(Logic.GetTile(VerticalRigthBorder, HorizontalSecondSpesialPlase));

            Logic.AddWall(WallVerticalFirst, WallHorizontalPlace, WallState);
            Logic.AddWall(WallVerticalSecond, WallHorizontalPlace, WallState);
            Logic.AddWall(WallVerticalThird, WallHorizontalPlace, WallState);
            Logic.AddWall(WallVerticalFive, WallHorizontalPlace, WallState);
            Logic.AddWall(WallVerticalSix, WallHorizontalPlace, WallState);
        }
    }
}