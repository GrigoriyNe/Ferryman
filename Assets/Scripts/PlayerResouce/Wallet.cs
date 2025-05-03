using System;
using UnityEngine;
using YG;

namespace PlayerResouce
{
    public class Wallet : MonoBehaviour
    {
        private const int BombForExplode = 1;

        [SerializeField] private VideoRewarder _rewardBomb;
        [SerializeField] private int _bombValueForReward = 2;
        [SerializeField] private Obstacle.ObstacleLogic _obstacle;
        [SerializeField] private SoungsGroup.Soungs _soungs;

        public event Action<int> ChangedMoney;

        public event Action<int> ChangedBomb;

        public int Money { get; private set; }

        public int Bomb { get; private set; }

        public int StartMoney { get; private set; } = 50;

        public int StartBomb { get; private set; } = 2;

        private void OnEnable()
        {
            _rewardBomb.RewardBomb += OnRewardBomb;
            _obstacle.BombUsed += RemoveBomb;
        }

        private void OnDisable()
        {
            _rewardBomb.RewardBomb -= OnRewardBomb;
            _obstacle.BombUsed -= RemoveBomb;
        }

        public void SetDefaultValue()
        {
            Money = StartMoney;
            Bomb = StartBomb;
            ChangedMoney?.Invoke(Money);
            ChangedBomb?.Invoke(Bomb);
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

        public void AddBomb(int value)
        {
            Bomb += value;
            ChangedBomb?.Invoke(Bomb);
            _soungs.PlayCoinPositiveSoung();
        }

        public void RemoveBomb()
        {
            Bomb -= BombForExplode;
            ChangedBomb?.Invoke(Bomb);
        }

        public bool IsEnoughBomb()
        {
            return Bomb > 0;
        }

        public void SetLoadValues(int money, int bombs)
        {
            Money = money;
            Bomb = bombs;

            ChangedMoney?.Invoke(Money);
            ChangedBomb?.Invoke(Bomb);
        }

        private void OnRewardBomb(string idReward)
        {
            AddBomb(_bombValueForReward);
        }
    }
}