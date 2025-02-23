using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopWindow : Window
{
    [SerializeField] private Button _item;
    [SerializeField] private Button _item2;
    [SerializeField] private Button _item3;
    [SerializeField] private CanvasGroup _canvasGroupOffer;
    [SerializeField] private Image _sellInfo;


    public event Action OnItem1Click;
    public event Action OnItem2Click;
    public event Action OnItem3Click;

    //private void Start()
    //{
    //    this.gameObject.SetActive(false);
    //}

    public override void OnEnabled()
    {
        _item.onClick.AddListener(OnButtonItem1Click);
    }

    public override void OnDisabled()
    {
        _item.onClick.RemoveListener(OnButtonItem1Click);
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

    //_canvasGroupOffer.SetActive(true);
    //_canvasGroupOffer.Open();

}