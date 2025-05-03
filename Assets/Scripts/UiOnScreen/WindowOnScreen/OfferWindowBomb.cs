using UnityEngine;

namespace WindowOnScreen
{
    public class OfferWindowBomb : WindowOnScreen.OfferWindow
    {
        private const int ValueBombForPay = 1;

        [SerializeField] private int _goldPrice;
        [SerializeField] private PlayerResouce.Wallet _wallet;

        public override void OnButtonGoldClick()
        {
            if (IsEnoughMoney(_goldPrice))
            {
                _wallet.AddBomb(ValueBombForPay);
            }
        }
    }
}
