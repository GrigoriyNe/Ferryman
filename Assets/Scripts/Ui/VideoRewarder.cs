using System;
using UnityEngine;

namespace YG
{
    public class VideoRewarder : MonoBehaviour
    {
        [SerializeField] private int _adID;

        public event Action<int> RewardBomb;

        private void OnEnable()
        {
            YandexGame.RewardVideoEvent += Rewarded;
        }

        private void OnDisable()
        {
            YandexGame.RewardVideoEvent -= Rewarded;
        }

        private void Rewarded(int id)
        {
            if (id == _adID)
                RewardBomb?.Invoke(id);
        }
    }
}


