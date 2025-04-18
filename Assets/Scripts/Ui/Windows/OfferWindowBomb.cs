using UnityEngine;

public class OfferWindowBomb : OfferWindow
{
    private const int ValueBombForPay = 1;

    [SerializeField] private int _goldPrice;
    [SerializeField] private Wallet _wallet;

    public override void OnButtonGoldClick()
    {
        if (IsEnoughMoney(_goldPrice))
        {
            _wallet.AddBomb(ValueBombForPay);
        }
    }
}
