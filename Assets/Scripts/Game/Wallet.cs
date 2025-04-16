using System;
using UnityEngine;
using YG;

public class Wallet : MonoBehaviour
{
    [SerializeField] private VideoRewarder _rewardBomb;
    [SerializeField] int _bombValueForReward = 2;
    [SerializeField] private ObstacleLogic _obstacle;
    [SerializeField] private Soungs _soungs;

    public event Action<int> ChangedMoney;
    public event Action<int> ChangedBomb;
    public event Action BombUse;

    public int Money { get; private set; }

    public int Bomb { get; private set; }
    public int StartMoney { get; private set; } = 50;
    public int StartBomb { get; private set; } = 2;


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

    private void OnRewardBomb(string _)
    {
        AddBomb(_bombValueForReward);
    }

    private void Start()
    {
        Money = StartMoney;
        Bomb = StartBomb;
        ChangedMoney?.Invoke(Money);
    }

    public void OnResetGame()
    {
        Money = 200;
        Bomb = 2;
        ChangedMoney?.Invoke(Money);
    }

    public void AddMoney(int value)
    {
        Money += value;
        ChangedMoney?.Invoke(Money);
    }

    public void RemoveMoney(int value)
    {
        Money -= value;
        ChangedMoney?.Invoke(Money);
    }

    public bool IsEnoughMoney(int value)
    {
        return Money - value >= 0;
    }

    public void AddBomb(int value)
    {
        Bomb += value;
        ChangedBomb?.Invoke(Bomb);
        _soungs.PlayCoinPositiveSoung();
    }

    public void RemoveBomb()
    {
        Bomb -= 1;
        ChangedBomb?.Invoke(Bomb);
    }

    public bool IsEnoughBomb()
    {
        return Bomb > 0;
    }
}
