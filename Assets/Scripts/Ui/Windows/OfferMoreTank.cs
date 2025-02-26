using UnityEngine;

public class OfferMoreTank : OfferWindow
{
    [SerializeField] private int _goldPrice;

    public override void OnButtonGoldClick()
    {
        if (TryPay(_goldPrice))
        {
            Shop.SellMoreTank();
        }

        Close();
    }
}