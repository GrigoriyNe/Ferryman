using System;
using UnityEngine;
using UnityEngine.UI;

public class OfferWindow : Window
{
    [SerializeField] private Button _gold;
    [SerializeField] private Button _revard;
    [SerializeField] private Game _game;

    //private void Start()
    //{
    //    this.gameObject.SetActive(false);
    //}

    public override void OnEnabled()
    {
        _gold.onClick.AddListener(OnButtonGoldClick);
        _revard.onClick.AddListener(OnButtonGoldRevard);
    }

    private void OnButtonGoldRevard()
    {
        throw new NotImplementedException();
    }

    public override void OnDisabled()
    {
        _gold.onClick.RemoveListener(OnButtonGoldClick);
        _revard.onClick.AddListener(OnButtonGoldRevard);
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
