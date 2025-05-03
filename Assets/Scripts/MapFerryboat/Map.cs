using UnityEngine;

namespace MapFerryboat
{
    public abstract class Map : MonoBehaviour
    {
        public const int Width = 24;
        public const int Height = 24;
        public const int RoadOffVerticalValue = Width - MiddleOffset;
        public const int MiddleOffset = 8;

        [SerializeField] protected MapLogic Logic;

        public int GetHeight() => Height;

        public abstract void CreateItemOnMap();

        public abstract void SetVaribleObstaclePlaces();

        public void Activate()
        {
            Logic.Init(Width, Height, RoadOffVerticalValue);
            gameObject.SetActive(true);
            CreateItemOnMap();
            SetVaribleObstaclePlaces();
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
            Logic.Deactivate();
        }
    }
}