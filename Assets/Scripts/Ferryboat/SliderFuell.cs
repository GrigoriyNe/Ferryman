using UnityEngine;
using UnityEngine.UI;

public class SliderFuell : MonoBehaviour
{
    [SerializeField] private Fueltank _tank;
    [SerializeField] private Slider _slider;

    private void Awake()
    {
        _slider.maxValue = _tank.Max;
        _slider.value = _slider.maxValue;
    }

    private void OnEnable()
    {
        _tank.Changed += ChangeSlider;
    }

    private void ChangeSlider(int value)
    {
        _slider.value = value;
    }
}
