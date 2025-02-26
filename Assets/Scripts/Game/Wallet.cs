using System;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    public event Action<int> Changed;
    public int Money { get; private set; }

    private void Start()
    {
        Money = 200;
        Changed?.Invoke(Money);
    }

    public void AddMoney(int value)
    {
        Money += value;
        Changed?.Invoke(Money);
    }

    public void RemoveMoney(int value)
    {
        Money -= value;
        Changed?.Invoke(Money);
    }

    public bool IsEnough(int value)
    {
        return Money - value > 0;
    }
}
