using UnityEngine;
using YG;

namespace PlayerResouce
{
    public class BombCount : Counter
    {
        private const int BombValueForReward = 2;
        private const int BombForExplode = 1;

        [SerializeField] private VideoRewarder _rewardBomb;
        [SerializeField] private Obstacle.ObstacleLogic _obstacle;

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

        public void RemoveBomb()
        {
            RemoveCount(BombForExplode);
        }

        private void OnRewardBomb(string idReward)
        {
            AddCount(BombValueForReward);
        }
    }
}