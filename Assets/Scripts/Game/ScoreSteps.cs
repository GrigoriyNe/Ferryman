﻿using System;
using UnityEngine;

public class ScoreSteps : MonoBehaviour
{
    [SerializeField] private Game _game;

    public event Action<int> Changed;

    public int StepsLeft { get; private set; }

    private void OnDisable()
    {
        StepsLeft = 0;
        Changed?.Invoke(0);
    }

    public void SetStartValue(int value)
    {
        StepsLeft = value;
        Changed?.Invoke(value);
    }

    public void ChangeOnOne()
    {
        StepsLeft -= 1;
        Changed?.Invoke(StepsLeft);
    
        if(StepsLeft == 0)
            _game.RoundOver();
    }
}