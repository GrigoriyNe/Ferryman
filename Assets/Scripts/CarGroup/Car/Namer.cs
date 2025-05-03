using System.Collections.Generic;
using UnityEngine;

namespace CarGroup
{
    public abstract class Namer : MonoBehaviour
    {
        public abstract string GetTextPlace(int vertical, int horizontal);
        public abstract void FillDictionary();

        protected Dictionary<int, string> _placesVertical;
        protected Dictionary<int, int> _placesHorizontal;

        public void OnEnable()
        {
            _placesVertical = new Dictionary<int, string>();
            _placesHorizontal = new Dictionary<int, int>();
            FillDictionary();
        }

        public Namer Get()
        {
            return this;
        }
    }
}