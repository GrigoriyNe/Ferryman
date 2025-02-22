using System;
using UnityEngine;
using UnityEngine.UI;

public class ShopWindow : Window
{
    [SerializeField] private Button _item;
    [SerializeField] private Button _item2;
    [SerializeField] private Button _item3;
    [SerializeField] private CanvasGroup _canvasGroupOffer;

    public event Action OnItem1Click;
    public event Action OnItem2Click;
    public event Action OnItem3Click;

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

    
    //_canvasGroupOffer.SetActive(true);
    //_canvasGroupOffer.Open();

}