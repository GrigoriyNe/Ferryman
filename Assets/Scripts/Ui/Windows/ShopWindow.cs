using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopWindow : Window
{
    [SerializeField] private Button _deleteOstacleButton;
    [SerializeField] private Button _deleteSpesialOstacleButton;
    [SerializeField] private Button _addedMaxFuletank;
    [SerializeField] private Button _smalerVarible;
    [SerializeField] private Image _sellInfo;

    public event Action OnItem1Click;
    public event Action OnItem2Click;
    public event Action OnItem3Click;
    public event Action OnItem4Click;

    public override void OnEnabled()
    {
        _deleteOstacleButton.onClick.AddListener(OnButtonItem1Click);
        _deleteSpesialOstacleButton.onClick.AddListener(OnButtonItem2Click);
        _addedMaxFuletank.onClick.AddListener(OnButtonItem3Click);
        _smalerVarible.onClick.AddListener(OnButtonItem4Click);
    }

    public override void OnDisabled()
    {
        _deleteOstacleButton.onClick.RemoveListener(OnButtonItem1Click);
        _deleteSpesialOstacleButton.onClick.RemoveListener(OnButtonItem2Click);
        _addedMaxFuletank.onClick.RemoveListener(OnButtonItem3Click);
        _smalerVarible.onClick.RemoveListener(OnButtonItem4Click);
    }

    public void OnButtonItem1Click()
    {
        OnItem1Click?.Invoke();
    }

    public void OnButtonItem2Click()
    {
        OnItem2Click?.Invoke();
    }

    public void OnButtonItem3Click()
    {
        OnItem3Click?.Invoke();
    }

    public void OnButtonItem4Click()
    {
        OnItem4Click?.Invoke();
    }

    public void ShowSellInfo()
    {
        _sellInfo.gameObject.SetActive(true);
        StartCoroutine(DeactivateImge());
    }

    private IEnumerator DeactivateImge()
    {
        yield return new WaitForSeconds(1.6f);

        _sellInfo.gameObject.SetActive(false);
        Close();
    }
}