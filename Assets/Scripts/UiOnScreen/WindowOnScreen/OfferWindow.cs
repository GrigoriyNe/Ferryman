﻿using UnityEngine;
using UnityEngine.UI;
using YG;

namespace WindowOnScreen
{
    public abstract class OfferWindow : Window
    {
        [SerializeField] private Button _gold;
        [SerializeField] private Button _revard;
        [SerializeField] private Game.GameLoop _game;
        [SerializeField] private StarterShow _videoRewarder;

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
            _videoRewarder.ButtonRewardClick();
        }

        public abstract void OnButtonGoldClick();

        public bool IsEnoughMoney(int value)
        {
            return _game.TryPay(value);
        }
    }
}