using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CameraButtonsChange : MonoBehaviour
{
    [SerializeField] private Button _up;
    [SerializeField] private Button _down;
    [SerializeField] private Button _left;
    [SerializeField] private Button _rigt;
    [SerializeField] private Button _bigger;
    [SerializeField] private Button _smaller;

    public event Action<int> ButtonVerticalClicked;
    public event Action<int> ButtonZoomClicked;
    public event Action<int> ButtonHorizontalClicked;

    private void Start()
    {
        _up.onClick.AddListener(OnUpClick);
        _down.onClick.AddListener(OnDownClick);
        _bigger.onClick.AddListener(OnBiggerClick);
        _smaller.onClick.AddListener(OnSmallerClick);
        _left.onClick.AddListener(OnHorizotalLeftClick);
        _rigt.onClick.AddListener(OnHorizotalRigtClick);
    }

    private void OnDisable()
    {
        _up.onClick.RemoveListener(OnUpClick);
        _down.onClick.RemoveListener(OnDownClick);
        _bigger.onClick.RemoveListener(OnBiggerClick);
        _smaller.onClick.RemoveListener(OnSmallerClick);
        _left.onClick.RemoveListener(OnHorizotalLeftClick);
        _rigt.onClick.RemoveListener(OnHorizotalRigtClick);
    }

    private void OnHorizotalRigtClick()
    {
        ButtonHorizontalClicked(1);
    }
    private void OnHorizotalLeftClick()
    {
        ButtonHorizontalClicked(-1); 
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
        ButtonVerticalClicked?.Invoke(-1);
    }

    private void OnUpClick()
    {
        ButtonVerticalClicked?.Invoke(1);
    }

    
}
