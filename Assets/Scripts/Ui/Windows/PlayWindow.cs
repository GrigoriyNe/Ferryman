using UnityEngine;
using UnityEngine.UI;

public class PlayWindow : Window
{
    [SerializeField] private Button _settings;
    [SerializeField] private Button _mute;
    [SerializeField] private Button _shop;
    [SerializeField] private Button _restart;
    [SerializeField] private Window _settingsWindow;
    [SerializeField] private ShopWindow _shopWindow;
    [SerializeField] private Game _game;

    public override void OnEnabled()
    {
        _shop.onClick.AddListener(OnButtonShopClick);
        _restart.onClick.AddListener(OnButtonRestartShopClick);
        _game.StartSceneDone += Activate;
        _game.FinishSceneStart += Deactivate;
        
        Deactivate();
    }

    public override void OnDisabled()
    {
        _shop.onClick.RemoveListener(OnButtonShopClick);
        _restart.onClick.RemoveListener(OnButtonRestartShopClick );
        _game.StartSceneDone -= Activate;
        _game.FinishSceneStart -= Deactivate;
    }

    private void Activate()
    {
        WindowGroup.interactable = true;
    }

    private void Deactivate()
    {
        WindowGroup.interactable = false;
    }

    private void OnButtonRestartShopClick()
    {
        _game.StepsOver();
    }

    private void OnButtonShopClick()
    {
        _shopWindow.Open();
    }

    //private void OnButtonMuteClick()
    //{
    //    _shopWindow.gameObject.SetActive(true);
    //    _shopWindow.Open();
    //}

    public void OnButtonSettingsClick()
    {
        //_settingsWindow.gameObject.SetActive(true);
        //_settingsWindow.Open();
    }
}
