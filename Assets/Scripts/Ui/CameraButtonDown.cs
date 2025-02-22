using System;
using UnityEngine;
using UnityEngine.UI;

public class CameraButtonDown : MonoBehaviour
{
    [SerializeField] private Button _down;

    public event Action ButtonClicked;

    private void OnEnable()
    {
        _down.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _down.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        ButtonClicked?.Invoke();
    }
}
