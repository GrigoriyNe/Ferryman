using UnityEngine;
using YG;

namespace YG
{
    public class StarterShow : MonoBehaviour
    {
        [SerializeField] private string _adID;

        private void OnEnable()
        {
            YG2.onOpenAnyAdv += OnOpen;
            YG2.onCloseRewardedAdv += OnClose;
            YG2.onErrorRewardedAdv += OnError;
        }

        private void OnDisable()
        {
            YG2.onOpenAnyAdv -= OnOpen;
            YG2.onCloseRewardedAdv -= OnClose;
            YG2.onErrorRewardedAdv -= OnError;
        }

        public void ButtonRewardClick()
        {
            YG2.RewardedAdvShow(_adID);
        }

        private void OnOpen()
        {
            Time.timeScale = 0;
        }

        private void OnClose()
        {
            Time.timeScale = 1;
        }

        private void OnError()
        {
            Time.timeScale = 1;
        }
    }
}

