using UnityEngine;

public class RestartStatisticInfoCounter : MonoBehaviour
{
    [SerializeField] private Game _game;
    [SerializeField] private ObstacleLogic _obstacleLogic;
    [SerializeField] private Wallet _wallet;

    private int _maxLevel = 0;
    private int _usedBomb = 0;
    private int _maxMoney = 0;

    private void OnEnable()
    {
        _wallet.ChangedMoney += OnChangedMoney;
        _obstacleLogic.BombUsed += OnChangedBomb;
        _game.LevelChange += OnLevelChange;
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