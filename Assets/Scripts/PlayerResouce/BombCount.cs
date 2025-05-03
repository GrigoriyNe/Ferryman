using System;
using UnityEngine;
using YG;

namespace PlayerResouce
{
    public class BombCount : MonoBehaviour
    {
        private const int BombValueForReward = 2;
        private const int BombForExplode = 1;

        [SerializeField] private VideoRewarder _rewardBomb;
        [SerializeField] private Obstacle.ObstacleLogic _obstacle;
        [SerializeField] private SoungsGroup.Soungs _soungs;

        public event Action<int> ChangedBomb;

        public int Bomb { get; private set; }

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
            Bomb = StartBomb;
            ChangedBomb?.Invoke(Bomb);
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

        public void SetLoadValues(int bombs)
        {
            Bomb = bombs;
            ChangedBomb?.Invoke(Bomb);
        }

        private void OnRewardBomb(string idReward)
        {
            AddBomb(BombValueForReward);
        }
    }
}