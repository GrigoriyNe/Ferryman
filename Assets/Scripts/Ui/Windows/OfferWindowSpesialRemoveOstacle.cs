using UnityEngine;

public class OfferWindowSpesialRemoveOstacle : OfferWindow
{
    [SerializeField] private int _goldPrice;

    public override void OnButtonGoldClick()
    {
        if (TryPay(_goldPrice))
        {
            Shop.SellSpesialRemoveObstacle();
        }

        Close();
    }
}
