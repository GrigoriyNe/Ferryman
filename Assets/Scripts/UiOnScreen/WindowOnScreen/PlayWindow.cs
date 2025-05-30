﻿using UnityEngine;
using UnityEngine.UI;

namespace WindowOnScreen
{
    public class PlayWindow : Window
    {
        [SerializeField] private Button _restart;
        [SerializeField] private Button _restartExecleyDone;
        [SerializeField] private Button _restartExecleyNone;
        [SerializeField] private Image _imageExecley;
        [SerializeField] private Counters.RewardCounter _rewardCounter;

        [SerializeField] private Button _useBomb;
        [SerializeField] private Game.GameLoop _game;

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
            _useBomb.onClick.RemoveListener(OnButtonUseBombClick);

            _settings.onClick.RemoveListener(OnButtonSettingsClick);
            _settingExit.onClick.RemoveListener(OnExitSettingClick);

            _leaderbod.onClick.RemoveListener(OnButtonLeaderbodClick);
            _leaderbodExit.onClick.RemoveListener(OnExitLeaderbodClick);

            _game.StartSceneDone -= Activate;
            _game.FinishSceneStart -= Deactivate;
        }

        public void OnButtonSettingsClick()
        {
            _settingCanvas.gameObject.SetActive(true);
            _settingExit.gameObject.SetActive(true);

            _leaderbodCanvas.gameObject.SetActive(false);
            _leaderbodExit.gameObject.SetActive(false);
        }

        public void OnButtonLeaderbodClick()
        {
            _leaderbodCanvas.gameObject.SetActive(true);
            _leaderbodExit.gameObject.SetActive(true);

            _settingCanvas.gameObject.SetActive(false);
            _settingExit.gameObject.SetActive(false);
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
            if (_rewardCounter.GetRewardValue() < 0)
            {
                _imageExecley.gameObject.SetActive(true);
                _restartExecleyDone.onClick.AddListener(OnRestartExecleyDoneClick);
                _restartExecleyNone.onClick.AddListener(OnRestartExecleyNoneClick);

                return;
            }

            _game.RoundOver();
        }

        private void OnRestartExecleyDoneClick()
        {
            _game.RoundOver();
            _imageExecley.gameObject.SetActive(false);
            _restartExecleyDone.onClick.RemoveListener(OnRestartExecleyDoneClick);
            _restartExecleyNone.onClick.RemoveListener(OnRestartExecleyNoneClick);
        }

        private void OnRestartExecleyNoneClick()
        {
            _imageExecley.gameObject.SetActive(false);
            _restartExecleyDone.onClick.RemoveListener(OnRestartExecleyDoneClick);
            _restartExecleyNone.onClick.RemoveListener(OnRestartExecleyNoneClick);
        }

        private void OnExitSettingClick()
        {
            _settingCanvas.gameObject.SetActive(false);
            _settingExit.gameObject.SetActive(false);
        }

        private void OnExitLeaderbodClick()
        {
            _leaderbodCanvas.gameObject.SetActive(false);
            _leaderbodExit.gameObject.SetActive(false);
        }
    }
}