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
    [SerializeField] private OfferWindow _offerSmalerVarible;
    [SerializeField] private OfferWindow _offerNewFerryboat;

    [Header(nameof(ShopWindow))]
    [SerializeField] private string _infoCleanTile;
    [SerializeField] private string _infoUpgradeSmalerVarible;
    [SerializeField] private string _infoUpgradeNewFerryboat;


    private void OnEnable()
    {
        _shopWindow.ButtonClicked += OnExitClicked;
        _shopWindow.OnItem1Click += OnDeleteBoxs;
        _shopWindow.OnItem2Click += OnDeleteSpesialBoxs;
        _shopWindow.OnItem4Click += OnSmallerVarible;
        _shopWindow.OnItem6Click += OnNewFerryboat;
    }

    private void OnDisable()
    {
        _shopWindow.ButtonClicked -= OnExitClicked;
        _shopWindow.OnItem1Click -= OnDeleteBoxs;
        _shopWindow.OnItem2Click -= OnDeleteSpesialBoxs;
        _shopWindow.OnItem4Click -= OnSmallerVarible;
        _shopWindow.OnItem5Click -= OnNewFerryboat;
    }

    public void SellRemoveObstacle()
    {
        _shopWindow.SetText(_infoCleanTile);
        _shopWindow.ShowSellInfo();
        _obstacle.ActivateClicked();
        _offer.ButtonClicked -= OnExitOffer;
    }

    public void SellSpesialRemoveObstacle()
    {
        _obstacle.ActivateSpesialClicked();
    }


    public void SellSmalerVarible()
    {
        _obstacle.SmalerVaribleCreating();
        _shopWindow.SetText(_infoUpgradeSmalerVarible);
        _shopWindow.ShowSellInfo();
        _offerSmalerVarible.ButtonClicked -= OnExitOffer;
    }


    public  void SellNewFerryboat()
    {
        _game.SetNextFerryboat();
        _shopWindow.ShowSellInfo();
        _shopWindow.SetText(_infoUpgradeNewFerryboat);
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
        _offerSmalerVarible.Close();
        _offerNewFerryboat.Close();
    }
}
