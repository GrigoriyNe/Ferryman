using TMPro;
using UnityEngine;

public class ViewSteps : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _stepsCount;
    [SerializeField] private ScoreSteps _steps;

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
        _stepsCount.text = value.ToString();
    }
}

