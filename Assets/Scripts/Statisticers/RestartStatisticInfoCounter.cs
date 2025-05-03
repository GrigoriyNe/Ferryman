using UnityEngine;
using YG;

namespace Statisticers
{
    public class RestartStatisticInfoCounter : MonoBehaviour
    {
        [SerializeField] private Game.GameProcess _game;
        [SerializeField] private Obstacle.ObstacleLogic _obstacleLogic;
        [SerializeField] private PlayerResouce.MoneyCount _wallet;

        private int _maxLevel = 0;
        private int _usedBomb = 0;
        private int _maxMoney = 0;

        private void OnEnable()
        {
            _wallet.ChangedMoney += OnChangedMoney;
            _obstacleLogic.BombUsed += OnChangedBomb;
            _game.LevelChange += OnLevelChange;

            _maxLevel = YG2.saves.Level;
            _usedBomb = YG2.saves.Bomb;
            _maxMoney = YG2.saves.Money;
        }

        private void OnDisable()
        {
            _wallet.ChangedMoney -= OnChangedMoney;
            _obstacleLogic.BombUsed -= OnChangedBomb;
            _game.LevelChange -= OnLevelChange;
        }

        public string GetLevelInfo()
        {
            int resultSumView = _maxLevel + 1;
            string result = resultSumView.ToString();
            _maxLevel = 0;

            return result;
        }

        public string GetUsedBombInfo()
        {
            string result = _usedBomb.ToString();
            _usedBomb = 0;

            return result;
        }

        public string GetWorkedMoneyInfo()
        {
            string result = _maxMoney.ToString();
            _maxMoney = 0;

            return result;
        }

        private void OnChangedMoney(int value)
        {
            if (_maxMoney < value)
                _maxMoney = value;
        }

        private void OnChangedBomb()
        {
            _usedBomb++;
        }

        private void OnLevelChange(int value)
        {
            _maxLevel = value;
        }
    }
}