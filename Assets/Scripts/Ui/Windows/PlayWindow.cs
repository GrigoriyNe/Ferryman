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

    [SerializeField] private Button _leaderbod;
    [SerializeField] private Button _leaderbodExit;
    [SerializeField] private GameObject _leaderbodCanvas;

    public override void OnEnabled()
    {
        _restart.onClick.AddListener(OnButtonRestartClick);
        _useBomb.onClick.AddListener(OnButtonUseBombClick);

        _settings.onClick.AddListener(OnButtonSettingsClick);
        _settingExit.onClick.AddListener(OnExitSettingClick);
        _settingExit.gameObject.SetActive(false);

        _leaderbod.onClick.AddListener(OnButtonLeaderbodClick);
        _leaderbodExit.onClick.AddListener(OnExitLeaderbodClick);
        _leaderbodExit.gameObject.SetActive(false);

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

        _leaderbod.onClick.RemoveListener(OnButtonLeaderbodClick);
        _leaderbodExit.onClick.RemoveListener(OnExitLeaderbodClick);

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
        _settingCanvas.gameObject.SetActive(true);
        _settingExit.gameObject.SetActive(true);
    }

    private void OnExitSettingClick()
    {
        _settingCanvas.gameObject.SetActive(false);
        _settingExit.gameObject.SetActive(false);
    }

    public void OnButtonLeaderbodClick()
    {
        _leaderbodCanvas.gameObject.SetActive(true);
        _leaderbodExit.gameObject.SetActive(true);
    }

    private void OnExitLeaderbodClick()
    {
        _leaderbodCanvas.gameObject.SetActive(false);
        _leaderbodExit.gameObject.SetActive(false);
    }
}
