using TMPro;
using UnityEngine;

public class ViewSteps : MonoBehaviour
{
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
        if (value == -1)
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

