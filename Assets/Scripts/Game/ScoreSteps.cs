using System;
using UnityEngine;

public class ScoreSteps : MonoBehaviour
{
    public event Action<int> Changed;

    public int StepsLeft { get; private set; }

    public void SetStartValue(int value)
    {
        StepsLeft = value;
        Changed?.Invoke(value);
    }

    public void ChangeOn(int value)
    {
        StepsLeft -= value;
        Changed?.Invoke(StepsLeft);
    }
}