using TMPro;
using UnityEngine;

public class ViewMoney : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moneyCount;
    [SerializeField] private Wallet _wallet;

    private void OnEnable()
    {
        _wallet.Changed += OnChanged;
    }

    private void OnDisable()
    {
        _wallet.Changed -= OnChanged;
    }

    private void OnChanged(int value)
    {
        _moneyCount.text = value.ToString();
    }
}

