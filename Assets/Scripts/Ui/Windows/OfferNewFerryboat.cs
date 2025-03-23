using UnityEngine;

public class OfferNewFerryboat : OfferWindow
{
    [SerializeField] private int _goldPrice;

    public override void OnButtonGoldClick()
    {
        if (TryPay(_goldPrice))
        {
            Shop.SellNewFerryboat();
        }
    }
}