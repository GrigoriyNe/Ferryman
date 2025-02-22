using System;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private ShopWindow _shopWindow;
    [SerializeField] private PlayWindow _playWindow;
    [SerializeField] private OfferWindow _offer;

    private void OnEnable()
    {
        _shopWindow.ButtonClicked += OnExitClicked;
        _shopWindow.OnItem1Click += OnItem1Click;
        _shopWindow.OnItem2Click += OnItem2Click;
        _shopWindow.OnItem3Click += OnItem3Click;
    }

    private void OnDisable()
    {
        _shopWindow.ButtonClicked -= OnExitClicked;
        _shopWindow.OnItem1Click -= OnItem1Click;
        _shopWindow.OnItem2Click -= OnItem2Click;
        _shopWindow.OnItem3Click -= OnItem3Click;
        _offer.ButtonClicked -= OnExitOffer;
    }

    private void OnItem1Click()
    {
        _offer.gameObject.SetActive(true);
        _offer.ButtonClicked += OnExitOffer;
    }

    private void OnItem2Click()
    {
        throw new NotImplementedException();
    }

    private void OnItem3Click()
    {
        throw new NotImplementedException();
    }

    private void OnExitClicked()
    {
        _shopWindow.Close();
        _playWindow.Open();
    }

    private void OnExitOffer()
    {
        _offer.Close();
    }
}
