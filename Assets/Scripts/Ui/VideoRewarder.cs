using System;
using UnityEngine;

namespace YG
{
    public class VideoRewarder : MonoBehaviour
    {
        [SerializeField] private string _adID;

        public event Action<string> RewardBomb;

        private void OnEnable()
        {
            YG2.onRewardAdv += Rewarded;
        }

        private void OnDisable()
        {
            YG2.onRewardAdv -= Rewarded;
        }

        private void Rewarded(string id)
        {
            if (id == _adID)
                RewardBomb?.Invoke(id);
        }
    }
}


