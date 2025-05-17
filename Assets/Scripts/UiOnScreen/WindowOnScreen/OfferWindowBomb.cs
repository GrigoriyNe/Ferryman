using UnityEngine;

namespace WindowOnScreen
{
    public class OfferWindowBomb : WindowOnScreen.OfferWindow
    {
        private const int ValueBombForPay = 1;

        [SerializeField] private int _goldPrice;
        [SerializeField] private PlayerResouce.BombCount _bomb;

        public override void OnButtonGoldClick()
        {
            if (IsEnoughMoney(_goldPrice))
            {
                _bomb.AddCount(ValueBombForPay);
            }
        }
    }
}
