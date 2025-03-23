using UnityEngine;

public class OfferImproveEngine : OfferWindow
{
    [SerializeField] private int _goldPrice;

    public override void OnButtonGoldClick()
    {
        if (TryPay(_goldPrice))
        {
            Shop.SellImproveEngine();
        }
    }
}
