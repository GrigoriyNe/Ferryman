using System;
using UnityEngine;
using YG;

namespace PlayerResouce
{
    public class MoneyCount : MonoBehaviour
    {
        [SerializeField] private SoungsGroup.Soungs _soungs;

        public event Action<int> ChangedMoney;

        public int Money { get; private set; }

        public int StartMoney { get; private set; } = 50;

        public void SetDefaultValue()
        {
            Money = StartMoney;
            ChangedMoney?.Invoke(Money);
        }

        public void AddMoney(int value)
        {
            Money += value;
            ChangedMoney?.Invoke(Money);
        }

        public void RemoveMoney(int value)
        {
            Money -= value;
            ChangedMoney?.Invoke(Money);
        }

        public bool IsEnoughMoney(int value)
        {
            return Money - value >= 0;
        }

        public void SetLoadValues(int money)
        {
            Money = money;

            ChangedMoney?.Invoke(Money);
        }
    }
}
