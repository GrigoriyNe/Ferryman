using UnityEngine;

namespace MapFerryboat
{
    public abstract class Map : MonoBehaviour
    {
        public abstract void Activate();

        public abstract void Deactivate();

        public abstract int GetHeight();

        public abstract void SetVaribleObstaclePlaces();
    }
}