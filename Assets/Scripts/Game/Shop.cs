using System;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private ShopWindow _shopWindow;
    [SerializeField] private ObstacleLogic _obstacle;
    [SerializeField] private Game _game;

    [SerializeField] private PlayWindow _playWindow;
    [SerializeField] private OfferWindow _offer;
    [SerializeField] private OfferWindow _offerSpesial;
    [SerializeField] private OfferWindow _offerMoreTank;

    private void OnEnable()
    {
        _shopWindow.ButtonClicked += OnExitClicked;
        _shopWindow.OnItem1Click += OnDeleteBoxs;
        _shopWindow.OnItem2Click += OnDeleteSpesialBoxs;
        _shopWindow.OnItem3Click += OnItem3Click;
    }

    private void OnDisable()
    {
        _shopWindow.ButtonClicked -= OnExitClicked;
        _shopWindow.OnItem1Click -= OnDeleteBoxs;
        _shopWindow.OnItem2Click -= OnDeleteSpesialBoxs;
        _shopWindow.OnItem3Click -= OnItem3Click;
        _offer.ButtonClicked -= OnExitOffer;
        _offerSpesial.ButtonClicked -= OnExitOffer;
    }

    public void SellRemoveObstacle()
    {
        _shopWindow.ShowSellInfo();
        _obstacle.ActivateClicked();
    }

    public void SellSpesialRemoveObstacle()
    {
        _shopWindow.ShowSellInfo();
        _obstacle.ActivateSpesialClicked();
    }

    public void SellMoreTank()
    {
        _game.GetTank().SetMoreTank();
        _shopWindow.Close();
    }

    private void OnDeleteBoxs()
    {
        _offer.gameObject.SetActive(true);
        _offer.ButtonClicked += OnExitOffer;
    }

    private void OnDeleteSpesialBoxs()
    {
        _offerSpesial.gameObject.SetActive(true);
        _offerSpesial.ButtonClicked += OnExitOffer;
    }

    private void OnItem2Click()
    {
        throw new NotImplementedException();
    }

    private void OnItem3Click()
    {
        _offerMoreTank.gameObject.SetActive(true);
        _offerMoreTank.ButtonClicked += OnExitOffer;
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
