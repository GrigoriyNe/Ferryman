using TMPro;
using UnityEngine;

namespace UiOnScreen
{
    public class ViewSteps : MonoBehaviour
    {
        private const int LowestThreshold = -1;

        [SerializeField] private TextMeshProUGUI _stepsCount;
        [SerializeField] private Counters.ScoreSteps _steps;
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
                _stepsCount.text = string.Empty;
            }
            else
            {
                _stepsCount.text = value.ToString();
                _animator.ActivateStep();
            }
        }
    }
}