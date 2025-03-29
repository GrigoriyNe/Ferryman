using System;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    public event Action<int> ChangedMoney;
    public event Action<int> ChangedBomb;
    public event Action BombUse;

    public int Money { get; private set; }

    public int Bomb { get; private set; }


    private void Start()
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
        return Money - value > 0;
    }

    public void AddBomb(int value)
    {
        Bomb += value;
        ChangedBomb?.Invoke(Bomb);
    }

    public void RemoveBomb()
    {
        Bomb -= 1;
        ChangedBomb?.Invoke(Bomb);
        BombUse?.Invoke();
    }

    public bool IsEnoughBomb()
    {
        return Bomb  > 0;
    }
}
