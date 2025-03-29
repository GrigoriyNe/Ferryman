using System;
using UnityEngine;

public class ScoreSteps : MonoBehaviour
{
    [SerializeField] private Game _game;

    public event Action<int> Changed;

    public int StepsLeft { get; private set; }

    public void SetStartValue(int value)
    {
        StepsLeft = value;
        Changed?.Invoke(value);
    }

    public void ChangeOnOne()
    {
        StepsLeft -= 1;
        Changed?.Invoke(StepsLeft);
    }
}