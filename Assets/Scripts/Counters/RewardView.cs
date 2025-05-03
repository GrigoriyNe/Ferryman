using System.Collections.Generic;
using UnityEngine;

namespace Counters
{
    public class RewardView : MonoBehaviour
    {
        private const int Offset = 1;

        [SerializeField] private List<Sprite> _positiveCoins;
        [SerializeField] private List<Sprite> _negativeCoins;
        [SerializeField] private List<Sprite> _neturalCoins;

        public Sprite GetPositiveView(int value)
        {
            return _positiveCoins[Mathf.Abs(value - Offset)];
        }

        public Sprite GetNegativeiveView(int value)
        {
            return _negativeCoins[Mathf.Abs(value + Offset)];
        }

        public Sprite GetNeturalView(int value)
        {
            return _neturalCoins[Mathf.Abs(value - Offset)];
        }
    }
}