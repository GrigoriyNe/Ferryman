using System;
using UnityEngine;
using UnityEngine.UI;

public class OfferWindow : Window
{
    [SerializeField] private Button _gold;
    [SerializeField] private Button _revard;
    [SerializeField] private Game _game;
    [SerializeField] private Shop _shop;
    
    public override void OnEnabled()
    {
        _gold.onClick.AddListener(OnButtonGoldClick);
        _revard.onClick.AddListener(OnButtonRevard);
    }
    public override void OnDisabled()
    {
        _gold.onClick.RemoveListener(OnButtonGoldClick);
        _revard.onClick.AddListener(OnButtonRevard);
    }

    private void OnButtonRevard()
    {
        throw new NotImplementedException();
    }

    private void OnButtonGoldClick()
    {
        if (TryPay(20))
        {
            _shop.SellRemoveObstacle();
        }

        Close();
    }

    private bool TryPay(int value)
    {
        return _game.TryPay(value);
    }

}
