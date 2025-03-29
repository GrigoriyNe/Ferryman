using UnityEngine;

public class OfferWindowSpesialRemoveOstacle : OfferWindow
{
    [SerializeField] private int _goldPrice;
    [SerializeField] private Wallet _wallet;

    public override void OnButtonGoldClick()
    {
        if (TryPay(_goldPrice))
        {
            _wallet.AddBomb(1);
            Close();
        }
    }
}
