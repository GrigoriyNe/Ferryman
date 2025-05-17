using UnityEngine;
using UnityEngine.UI;

namespace UiOnScreen
{
    public class OfferRestartButton : MonoBehaviour
    {
        [SerializeField] private Game.GameLoop _game;
        [SerializeField] private Button _restart;

        private void OnEnable()
        {
            _restart.onClick.AddListener(OnRestartClick);
        }

        private void OnDisable()
        {
            _restart.onClick.RemoveListener(OnRestartClick);
        }

        private void OnRestartClick()
        {
            _game.Fail();
        }
    }
}