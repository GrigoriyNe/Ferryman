using TMPro;
using UnityEngine;

public class ViewResources : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moneyCount;
    [SerializeField] private TextMeshProUGUI _dollarCount;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private AnimationResources _animator;

    private void OnEnable()
    {
        _wallet.ChangedMoney += OnChangedMoney;
        _wallet.ChangedDollars += OnChangedDollar;
    }

    private void OnDisable()
    {
        _wallet.ChangedMoney -= OnChangedMoney;
        _wallet.ChangedDollars -= OnChangedDollar;
    }

    private void OnChangedMoney(int value)
    {
        _moneyCount.text = value.ToString();
        _animator.ActivateMoney();
    }

    private void OnChangedDollar(int value)
    {
        _dollarCount.text = value.ToString();
        _animator.ActivateDollars();
    }
}
