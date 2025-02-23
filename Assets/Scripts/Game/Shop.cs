using System;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private ShopWindow _shopWindow;
    [SerializeField] private PlayWindow _playWindow;
    [SerializeField] private OfferWindow _offer;
    [SerializeField] private ObstacleLogic _obstacle;

    private void OnEnable()
    {
        _shopWindow.ButtonClicked += OnExitClicked;
        _shopWindow.OnItem1Click += OnDeleteBoxs;
        _shopWindow.OnItem2Click += OnItem2Click;
        _shopWindow.OnItem3Click += OnItem3Click;
    }

    private void OnDisable()
    {
        _shopWindow.ButtonClicked -= OnExitClicked;
        _shopWindow.OnItem1Click -= OnDeleteBoxs;
        _shopWindow.OnItem2Click -= OnItem2Click;
        _shopWindow.OnItem3Click -= OnItem3Click;
        _offer.ButtonClicked -= OnExitOffer;
    }

    public void SellRemoveObstacle()
    {
        _shopWindow.ShowSellInfo();
        _obstacle.Activate();
    }

    private void OnDeleteBoxs()
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
