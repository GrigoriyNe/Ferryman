using System;
using UnityEngine;

public class ScoreSteps : MonoBehaviour
{
    [SerializeField] private Game _game;

    public event Action<int> Changed;

    public int StepsLeft { get; private set; }

    private void OnDisable()
    {
        StepsLeft = 0;
        Changed?.Invoke(StepsLeft);
    }

    public void SetStartValue(int value)
    {
        StepsLeft = value;
        Changed?.Invoke(StepsLeft - 1);
    }

    public void ChangeOnOne()
    {
        StepsLeft -= 1;
        Changed?.Invoke(StepsLeft - 1);
    
        if(StepsLeft == 0)
            _game.RoundOver();
    }
}