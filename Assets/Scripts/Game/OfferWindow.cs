using System;
using UnityEngine;
using UnityEngine.UI;

public class OfferWindow : Window
{
    [SerializeField] private Button _gold;
    [SerializeField] private Button _playWindow;
    [SerializeField] private Game _game;

    public override void OnEnabled()
    {
        _gold.onClick.AddListener(OnButtonGoldClick);
    }

    public override void OnDisabled()
    {
        _gold.onClick.RemoveListener(OnButtonGoldClick);
    }

    private void OnButtonGoldClick()
    {
        TryPay(20);
    }

    private void TryPay (int value)
    {
        _game.TryPay(value);
    }
}
