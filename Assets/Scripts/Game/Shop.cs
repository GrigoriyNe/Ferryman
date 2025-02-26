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
    [SerializeField] private OfferWindow _offerSmalerVarible;

    private void OnEnable()
    {
        _shopWindow.ButtonClicked += OnExitClicked;
        _shopWindow.OnItem1Click += OnDeleteBoxs;
        _shopWindow.OnItem2Click += OnDeleteSpesialBoxs;
        _shopWindow.OnItem3Click += OnBiggerTank;
        _shopWindow.OnItem4Click += OnSmallerVarible;
    }

    private void OnDisable()
    {
        _shopWindow.ButtonClicked -= OnExitClicked;
        _shopWindow.OnItem1Click -= OnDeleteBoxs;
        _shopWindow.OnItem2Click -= OnDeleteSpesialBoxs;
        _shopWindow.OnItem3Click -= OnBiggerTank;
        _shopWindow.OnItem4Click -= OnSmallerVarible;
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

    public void SellSmalerVarible()
    {
        _obstacle.SmalerVaribleCreating();
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

    private void OnSmallerVarible()
    {
        _offerSmalerVarible.gameObject.SetActive(true);
        _offerSmalerVarible.ButtonClicked += OnExitOffer;

    }

    private void OnBiggerTank()
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
