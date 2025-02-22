using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayWindow : Window
{
    [SerializeField] private Button _settings;
    [SerializeField] private Button _mute;
    [SerializeField] private Button _shop;
    [SerializeField] private Window _settingsWindow;
    [SerializeField] private ShopWindow _shopWindow;

    public override void OnEnabled()
    {
        _shop.onClick.AddListener(OnButtonShopClick);
    }

    public override void OnDisabled()
    {
        _shop.onClick.RemoveListener(OnButtonShopClick);
    }

    private void OnButtonShopClick()
    {
        
        _shopWindow.Open();
      //  Close();
    }

    //private void OnButtonMuteClick()
    //{
    //    _shopWindow.gameObject.SetActive(true);
    //    _shopWindow.Open();
    //}

    public void OnButtonSettingsClick()
    {
        _settingsWindow.gameObject.SetActive(true);
        _settingsWindow.Open();
    }
}
