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
    [SerializeField] private OfferWindow _offerImproveEngine;
    [SerializeField] private OfferWindow _offerNewFerryboat;

    private void OnEnable()
    {
        _shopWindow.ButtonClicked += OnExitClicked;
        _shopWindow.OnItem1Click += OnDeleteBoxs;
        _shopWindow.OnItem2Click += OnDeleteSpesialBoxs;
        _shopWindow.OnItem3Click += OnBiggerTank;
        _shopWindow.OnItem4Click += OnSmallerVarible;
        _shopWindow.OnItem5Click += OnImproveEngine;
        _shopWindow.OnItem6Click += OnNewFerryboat;
    }

    private void OnDisable()
    {
        _shopWindow.ButtonClicked -= OnExitClicked;
        _shopWindow.OnItem1Click -= OnDeleteBoxs;
        _shopWindow.OnItem2Click -= OnDeleteSpesialBoxs;
        _shopWindow.OnItem3Click -= OnBiggerTank;
        _shopWindow.OnItem4Click -= OnSmallerVarible;
        _shopWindow.OnItem5Click -= OnImproveEngine;
        _shopWindow.OnItem5Click -= OnNewFerryboat;
    }

    public void SellRemoveObstacle()
    {
        _shopWindow.ShowSellInfo();
        _obstacle.ActivateClicked();
        _offer.ButtonClicked -= OnExitOffer;
    }

    public void SellSpesialRemoveObstacle()
    {
        _shopWindow.ShowSellInfo();
        _obstacle.ActivateSpesialClicked();
        _offerSpesial.ButtonClicked -= OnExitOffer;
    }

    public void SellMoreTank()
    {
        _game.GetTank().SetMoreTank();
        _shopWindow.Close();
        _offerMoreTank.ButtonClicked -= OnExitOffer;
    }

    public void SellSmalerVarible()
    {
        _obstacle.SmalerVaribleCreating();
        _shopWindow.Close();
        _offerSmalerVarible.ButtonClicked -= OnExitOffer;
    }

    public void SellImproveEngine()
    {
        _game.GetTank().ImproveEngine();
        _shopWindow.Close();
        _offerImproveEngine.ButtonClicked -= OnExitOffer;
    }

    public  void SellNewFerryboat()
    {
        _game.SetNextFerryboat();
        _offerNewFerryboat.ButtonClicked -= OnExitOffer;
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

    private void OnImproveEngine()
    {

        _offerImproveEngine.gameObject.SetActive(true);
        _offerImproveEngine.ButtonClicked += OnExitOffer;
    }

    private void OnNewFerryboat()
    {

        _offerNewFerryboat.gameObject.SetActive(true);
        _offerNewFerryboat.ButtonClicked += OnExitOffer;
    }

    private void OnExitClicked()
    {
        _shopWindow.Close();
        _playWindow.Open();
    }

    private void OnExitOffer()
    {
        _offer.Close();
        _offerSpesial.Close();
        _offerMoreTank.Close();
        _offerSmalerVarible.Close();
        _offerImproveEngine.Close();
        _offerNewFerryboat.Close();
    }
}
