using UnityEngine;

public class OfferWindowRemoveOstacle : OfferWindow
{
    [SerializeField] private int _goldPrice;

    public override void OnButtonGoldClick()
    {
        if (TryPay(_goldPrice))
        {
            Shop.SellRemoveObstacle();
        }
    }
}
