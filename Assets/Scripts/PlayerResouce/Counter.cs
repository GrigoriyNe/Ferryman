using System;
using UnityEngine;

namespace PlayerResouce
{
    public class Counter : MonoBehaviour
    {
        [SerializeField] private int _startValue;

        public event Action<int> Changed;

        public int ItemCount { get; private set; }

        public int StartCount => _startValue;

        public void SetDefaultValue()
        {
            ItemCount = StartCount;
            Changed?.Invoke(ItemCount);
        }

        public void AddCount(int value)
        {
            ItemCount += value;
            Changed?.Invoke(ItemCount);
        }

        public void RemoveCount(int value)
        {
            ItemCount -= value;
            Changed?.Invoke(ItemCount);
        }

        public bool IsEnougCount(int value)
        {
            return ItemCount - value >= 0;
        }

        public void SetLoadValues(int value)
        {
            ItemCount = value;

            Changed?.Invoke(ItemCount);
        }
    }
}