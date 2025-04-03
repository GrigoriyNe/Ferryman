using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayWindow : Window
{
    [SerializeField] private Button _restart;
    [SerializeField] private Button _useBomb;
    [SerializeField] private Game _game;

    [SerializeField] private Button _settings;
    [SerializeField] private Button _settingExit;
    [SerializeField] private Canvas _settingCanvas;

    public override void OnEnabled()
    {
        _restart.onClick.AddListener(OnButtonRestartClick);
        _useBomb.onClick.AddListener(OnButtonUseBombClick);
        _settings.onClick.AddListener(OnButtonSettingsClick);
        _settingExit.onClick.AddListener(OnExitSettingClick);

        _game.StartSceneDone += Activate;
        _game.FinishSceneStart += Deactivate;

        Deactivate();
    }

    public override void OnDisabled()
    {
        _restart.onClick.RemoveListener(OnButtonRestartClick);
        _useBomb.onClick.AddListener(OnButtonUseBombClick);
        _settings.onClick.RemoveListener(OnButtonSettingsClick);
        _settingExit.onClick.RemoveListener(OnExitSettingClick);

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

    public void OnButtonSettingsClick()
    {
        if (_settingCanvas.gameObject.activeSelf)
            _settingCanvas.gameObject.SetActive(false);

        _settingCanvas.gameObject.SetActive(true);
    }

    private void OnExitSettingClick()
    {
        _settingCanvas.gameObject.SetActive(false);
    }
}
