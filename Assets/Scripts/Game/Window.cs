using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class Window : MonoBehaviour
{
    [SerializeField] protected CanvasGroup WindowGroup;
    [SerializeField] protected  Button ActionButton;

    public event Action ButtonClicked;

    private void OnEnable()
    {
        ActionButton.onClick.AddListener(OnButtonClick);
        OnEnabled();
    }

    private void OnDisable()
    {
        ActionButton.onClick.RemoveListener(OnButtonClick);
        OnDisabled();
    }

    public abstract void OnEnabled();
    public abstract void OnDisabled();

    public void Open()
    {
       this.gameObject.SetActive(true);
    }

    public void Close()
    {
        this.gameObject.SetActive(false);
    }

    protected void OnButtonClick()
    {
        ButtonClicked?.Invoke();
    }
}
