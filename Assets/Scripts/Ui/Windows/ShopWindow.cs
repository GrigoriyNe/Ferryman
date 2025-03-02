using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopWindow : Window
{
    [SerializeField] private Button _deleteOstacleButton;
    [SerializeField] private Button _deleteSpesialOstacleButton;
    [SerializeField] private Button _addedMaxFuletank;
    [SerializeField] private Button _smalerVarible;
    [SerializeField] private Button _improveEngine;
    [SerializeField] private Button _newFerryboat;
    [SerializeField] private Image _sellInfo;
    [SerializeField] private TextMeshProUGUI _sellTextInfo;

    public event Action OnItem1Click;
    public event Action OnItem2Click;
    public event Action OnItem3Click;
    public event Action OnItem4Click;
    public event Action OnItem5Click;
    public event Action OnItem6Click;

    public override void OnEnabled()
    {
        _deleteOstacleButton.onClick.AddListener(OnButtonItem1Click);
        _deleteSpesialOstacleButton.onClick.AddListener(OnButtonItem2Click);
        _addedMaxFuletank.onClick.AddListener(OnButtonItem3Click);
        _smalerVarible.onClick.AddListener(OnButtonItem4Click);
        _improveEngine.onClick.AddListener(OnButtonItem5Click);
        _newFerryboat.onClick.AddListener(OnButtonItem6Click);
    }

    public override void OnDisabled()
    {
        _deleteOstacleButton.onClick.RemoveListener(OnButtonItem1Click);
        _deleteSpesialOstacleButton.onClick.RemoveListener(OnButtonItem2Click);
        _addedMaxFuletank.onClick.RemoveListener(OnButtonItem3Click);
        _smalerVarible.onClick.RemoveListener(OnButtonItem4Click);
        _improveEngine.onClick.RemoveListener(OnButtonItem5Click);
        _newFerryboat.onClick.RemoveListener(OnButtonItem6Click);
    }

    public void ShowSellInfo()
    {
        _sellInfo.gameObject.SetActive(true);
        StartCoroutine(DeactivateImge());
    }

    public void SetText(string infoUpgradeMoreTank)
    {
        _sellTextInfo.text = infoUpgradeMoreTank;
    }

    private void OnButtonItem1Click()
    {
        OnItem1Click?.Invoke();
    }

    private void OnButtonItem2Click()
    {
        OnItem2Click?.Invoke();
    }

    private void OnButtonItem3Click()
    {
        OnItem3Click?.Invoke();
    }

    private void OnButtonItem4Click()
    {
        OnItem4Click?.Invoke();
    }


    private void OnButtonItem5Click()
    {
        OnItem5Click?.Invoke();
    }

    private void OnButtonItem6Click()
    {
        OnItem6Click?.Invoke();
    }

    private IEnumerator DeactivateImge()
    {
        yield return new WaitForSeconds(1.6f);

        _sellInfo.gameObject.SetActive(false);
        Close();
    }

    
}