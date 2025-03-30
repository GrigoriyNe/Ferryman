using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayWindow : Window
{
    [SerializeField] private Button _settings;
    [SerializeField] private Button _mute;
    [SerializeField] private Button _restart;
    [SerializeField] private Button _useBomb;
    [SerializeField] private Window _settingsWindow;
    [SerializeField] private Game _game;

    public override void OnEnabled()
    {
        _restart.onClick.AddListener(OnButtonRestartClick);
        _useBomb.onClick.AddListener(OnButtonUseBombClick);

        _game.StartSceneDone += Activate;
        _game.FinishSceneStart += Deactivate;
        
        Deactivate();
    }

    public override void OnDisabled()
    {
        _restart.onClick.RemoveListener(OnButtonRestartClick);
        _useBomb.onClick.AddListener(OnButtonUseBombClick);

        _game.StartSceneDone -= Activate;
        _game.FinishSceneStart -= Deactivate;
    }

    private void OnButtonUseBombClick()
    {
        _game.TryUseBomb();
    }

    private void Activate()
    {
        WindowGroup.interactable = true;
    }

    private void Deactivate()
    {
        WindowGroup.interactable = false;
    }

    private void OnButtonRestartClick()
    {
        _game.RoundOver();
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
