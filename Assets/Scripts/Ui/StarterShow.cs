using UnityEngine;
using UnityEngine.UI;
using YG;

namespace YG
{
    public class StarterShow : MonoBehaviour
    {
        [SerializeField] private int _adID;
        [SerializeField] private Button _actionButton;

        private void OnEnable()
        {
            YandexGame.OpenVideoEvent += OnOpen;
            YandexGame.CloseVideoEvent += OnClose;
            YandexGame.ErrorVideoEvent += OnError;
        }

        private void OnDisable()
        {
            YandexGame.OpenVideoEvent -= OnOpen;
            YandexGame.CloseVideoEvent -= OnClose;
            YandexGame.ErrorVideoEvent -= OnError;
        }

        private void Start()
        {
            _actionButton.onClick.AddListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            YandexGame.RewVideoShow(_adID);
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

