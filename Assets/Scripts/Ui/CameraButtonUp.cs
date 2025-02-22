using System;
using UnityEngine;
using UnityEngine.UI;

public class CameraButtonUp : MonoBehaviour
{
    [SerializeField] private Button _up;

    public event Action ButtonClicked;

    private void OnEnable()
    {
        _up.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _up.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        ButtonClicked?.Invoke();
    }
}
