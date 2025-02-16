using UnityEngine;

public class Wallet : MonoBehaviour
{
    public int Money { get; private set; }

    private void OnEnable()
    {
        Money = 100;
    }

    public void AddMoney(int value)
    {
        Money += value;
    }

    public void RemoveMoney(int value)
    {
        Money -= value;
    }

    public bool IsEnough(int value)
    {
        return Money - value > 0;
    }
}
