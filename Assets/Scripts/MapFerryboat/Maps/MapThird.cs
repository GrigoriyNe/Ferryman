using UnityEngine;

namespace MapFerryboat
{
    public class MapThird : Map
    {
        private const int Width = 24;
        private const int Height = 24;

        private const int RoadOffVerticalValue = Width - MiddleOffset;
        private const int MiddleOffset = 8;
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

        [SerializeField] private MapLogic _logic;
        [SerializeField] private Obstacle.ObstacleLogic _obstaleLogic;

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
            _logic.SetVaribleObstaclePlaces(LowestThresholdX, VerticalRigthBorder,
                VaribleObsaleHorizontalLower, VaribleObsaleHorizontalHeigth);
        }

        private void CreateItemOnMap()
        {
            for (int i = 0; i < RoadOffVerticalLowerValue; i++)
            {
                _logic.AddVoid(RigthStartPole + VerticalOffset, i);
                _logic.AddVoid(RigthStartPole + VerticalOffset + VerticalOffset, i);
            }

            for (int i = VerticalRigthBorder + VerticalOffset; i < Height; i++)
                for (int j = 0; j < Width; j++)
                    _logic.AddVoid(i, j);

            for (int i = VerticalOffset; i < VerticalRigthBorder; i++)
                for (int j = RoadOffVerticalLowerValue + VerticalOffset; j < Width; j++)
                    _logic.AddCarFinishPoint(i, j);

            for (int i = VerticalOffset; i < VerticalRigthBorder - VerticalOffset; i++)
            {
                _logic.AddCarStartPoint(i, StartVertcalPlase);
                _logic.AddSpesialCarStartPoint(i, StartVertcalPlase);
            }

            _logic.AddSpesialCarFinishPoint(VerticalRigthBorder, HorizontalFirstSpesialPlase);
            _logic.AddSpesialCarFinishPoint(VerticalRigthBorder, HorizontalSecondSpesialPlase);
            _logic.AddSpesialCarFinishPoint(VerticalRigthBorder, HorizontalThirdSpesialPlase);
            _logic.AddSpesialCarFinishPoint(VerticalLeftBorder, HorizontalFirstSpesialPlase);
            _logic.AddSpesialCarFinishPoint(VerticalLeftBorder, HorizontalSecondSpesialPlase);
            _logic.AddSpesialCarFinishPoint(VerticalLeftBorder, HorizontalThirdSpesialPlase);

            for (int i = VerticalOffset; i < VerticalRigthBorder; i++)
                _obstaleLogic.SetBlockedFinishPlace(_logic.GetTile(i, Height - HeightOffset));

            _obstaleLogic.SetSpesialFinishPlace(_logic.GetTile(VerticalLeftBorder, HorizontalFirstSpesialPlase));
            _obstaleLogic.SetSpesialFinishPlace(_logic.GetTile(VerticalLeftBorder, HorizontalSecondSpesialPlase));
            _obstaleLogic.SetSpesialFinishPlace(_logic.GetTile(VerticalLeftBorder, HorizontalThirdSpesialPlase));
            _obstaleLogic.SetSpesialFinishPlace(_logic.GetTile(VerticalRigthBorder, HorizontalFirstSpesialPlase));
            _obstaleLogic.SetSpesialFinishPlace(_logic.GetTile(VerticalRigthBorder, HorizontalSecondSpesialPlase));

            _logic.AddWall(WallVerticalFirst, WallHorizontalPlace, WallState);
            _logic.AddWall(WallVerticalSecond, WallHorizontalPlace, WallState);
            _logic.AddWall(WallVerticalThird, WallHorizontalPlace, WallState);
            _logic.AddWall(WallVerticalFive, WallHorizontalPlace, WallState);
            _logic.AddWall(WallVerticalSix, WallHorizontalPlace, WallState);
        }
    }
}