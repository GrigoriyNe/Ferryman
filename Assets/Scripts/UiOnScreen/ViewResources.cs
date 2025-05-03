using TMPro;
using UnityEngine;

namespace UiOnScreen
{
    public class ViewResources : MonoBehaviour
    {
        private const int StartBombsValue = 2;

        [SerializeField] private TextMeshProUGUI _moneyCount;
        [SerializeField] private TextMeshProUGUI _bombCount;
        [SerializeField] private PlayerResouce.MoneyCount _money;
        [SerializeField] private PlayerResouce.BombCount _bomb;
        [SerializeField] private AnimationResources _animator;
        [SerializeField] private Obstacle.ObstacleLogic _obstacleLogic;

        private int _bombsValue = StartBombsValue;

        private void OnEnable()
        {
            _money.ChangedMoney += OnChangedMoney;
            _bomb.ChangedBomb += OnChangedCountBomb;
            _obstacleLogic.BombUsed += OnDeactivateBomb;
            _obstacleLogic.BombCanceled += OnDeactivateBomb;
            _obstacleLogic.BombUse += OnActivatedBomb;
        }

        private void OnDisable()
        {
            _money.ChangedMoney -= OnChangedMoney;
            _bomb.ChangedBomb -= OnChangedCountBomb;
            _obstacleLogic.BombUsed -= OnDeactivateBomb;
            _obstacleLogic.BombCanceled -= OnDeactivateBomb;
            _obstacleLogic.BombUse -= OnActivatedBomb;
        }

        private void OnChangedCountBomb(int value)
        {
            _bombsValue = value;
            _bombCount.text = _bombsValue.ToString();
        }

        private void OnActivatedBomb()
        {
            _animator.ActivateBombUi();
        }

        private void OnDeactivateBomb()
        {
            _bombCount.text = _bombsValue.ToString();
            _animator.DeactivateBombUi();
        }

        private void OnChangedMoney(int value)
        {
            _moneyCount.text = value.ToString();
            _animator.ActivateMoney();
        }
    }
}