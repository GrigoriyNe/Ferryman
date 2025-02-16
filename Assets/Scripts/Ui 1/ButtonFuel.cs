using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFuel : MonoBehaviour
{
   [SerializeField] private Button _fuel;

    public event Action ButtonClicked;

    private void OnEnable()
    {
        _fuel.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _fuel.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        ButtonClicked?.Invoke();
    }
}
