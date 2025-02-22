using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonShop : MonoBehaviour
{
    [SerializeField] private Button _shop;

    public event Action ButtonClicked;

    private void OnEnable()
    {
        _shop.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _shop.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        ButtonClicked?.Invoke();
    }
}
