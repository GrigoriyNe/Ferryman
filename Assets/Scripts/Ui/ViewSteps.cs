﻿using TMPro;
using UnityEngine;

public class ViewSteps : MonoBehaviour
{
    private const int LowestThreshold = -1;

    [SerializeField] private TextMeshProUGUI _stepsCount;
    [SerializeField] private ScoreSteps _steps;
    [SerializeField] private AnimationResources _animator;

    private void OnEnable()
    {
        _steps.Changed += OnChanged;
    }

    private void OnDisable()
    {
        _steps.Changed -= OnChanged;
    }

    private void OnChanged(int value)
    {
        if (value == LowestThreshold)
        {
            _stepsCount.text = "";
        }
        else
        {
            _stepsCount.text = value.ToString();
            _animator.ActivateStep();
        }
    }
}

