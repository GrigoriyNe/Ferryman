using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CameraButtonsChange : MonoBehaviour
{
    [SerializeField] private Button _up;
    [SerializeField] private Button _down;
    [SerializeField] private Button _bigger;
    [SerializeField] private Button _smaller;

    public event Action<int> ButtonClicked;
    public event Action<int> ButtonZoomClicked;

    private void Start()
    {
        _up.onClick.AddListener(OnUpClick);
        _down.onClick.AddListener(OnDownClick);
        _bigger.onClick.AddListener(OnBiggerClick);
        _smaller.onClick.AddListener(OnSmallerClick);
    }

    private void OnDisable()
    {
        _up.onClick.RemoveListener(OnUpClick);
        _down.onClick.RemoveListener(OnDownClick);
        _bigger.onClick.RemoveListener(OnBiggerClick);
        _smaller.onClick.RemoveListener(OnSmallerClick);
    }

    private void OnSmallerClick()
    {
        ButtonZoomClicked?.Invoke(1);
    }

    private void OnBiggerClick()
    {
        ButtonZoomClicked?.Invoke(-1);
    }

    private void OnDownClick()
    {
        ButtonClicked?.Invoke(-1);
    }

    private void OnUpClick()
    {
        ButtonClicked?.Invoke(1);
    }

    
}
