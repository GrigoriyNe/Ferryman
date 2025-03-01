using UnityEngine;

public class OfferSmalerVarible : OfferWindow
{
    [SerializeField] private int _goldPrice;

    public override void OnButtonGoldClick()
    {
        if (TryPay(_goldPrice))
        {
            Shop.SellSmalerVarible();
        }

        Close();
    }
}
