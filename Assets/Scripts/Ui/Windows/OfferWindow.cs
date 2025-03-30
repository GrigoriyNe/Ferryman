using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class OfferWindow : Window
{
    [SerializeField] private Button _gold;
    [SerializeField] private Button _revard;
    [SerializeField] private Game _game;
    
    public override void OnEnabled()
    {
        _gold.onClick.AddListener(OnButtonGoldClick);
        _revard.onClick.AddListener(OnButtonRevard);
    }
    public override void OnDisabled()
    {
        _gold.onClick.RemoveListener(OnButtonGoldClick);
        _revard.onClick.RemoveListener(OnButtonRevard);
    }

    public void OnButtonRevard()
    {
        throw new NotImplementedException();
    }

    public abstract void OnButtonGoldClick();

    public bool TryPay(int value)
    {
        return _game.TryPay(value);
    }
}
