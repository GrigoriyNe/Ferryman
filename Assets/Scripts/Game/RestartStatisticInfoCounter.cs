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

    private void OnChangedMoney(int value)
    {
        _maxMoney += value;
    }

    private void OnChangedBomb()
    {
        _usedBomb++;
    }

    private void OnLevelChange(int value)
    {
        _maxLevel = value;
    }

    public string GetLevelInfo()
    {
        string result = _maxLevel.ToString();
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
        int resultSum = _maxMoney - _wallet.StartMoney;
        string result = resultSum.ToString();
        _maxMoney = 0;

        return result;
    }
}