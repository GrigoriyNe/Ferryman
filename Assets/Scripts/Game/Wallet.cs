using System;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    public event Action<int> ChangedMoney;
    public event Action<int> ChangedDollars;
    public int Money { get; private set; }
    public int Dollars { get; private set; }

    private void Start()
    {
        Money = 200;
        Dollars = 5;
        ChangedMoney?.Invoke(Money);
        ChangedDollars?.Invoke(Dollars);
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
    public void AddDollars(int value)
    {
        Dollars += value;
        ChangedDollars?.Invoke(Money);
    }

    public void RemoveDollars(int value)
    {
        Dollars -= value;
        ChangedDollars?.Invoke(Dollars);
    }

    public bool IsEnoughMoney(int value)
    {
        return Money - value > 0;
    }

    public bool IsEnoughDillars(int value)
    {
        return Dollars - value > 0;
    }
}
