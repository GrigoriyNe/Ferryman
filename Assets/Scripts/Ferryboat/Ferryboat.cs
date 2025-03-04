using System;
using System.Collections;
using UnityEngine;

public class Ferryboat : MonoBehaviour
{
    private const int FuelCoust = 1;

    [SerializeField] private Game _game;
    [SerializeField] private Map _map;
    [SerializeField] private NumberingPlaceText _numberingText;
    [SerializeField] private GameObject _ferryboatBackground;
    [SerializeField] private WindowBlind _blind;
    [SerializeField] private FerryboatAnimator _animator;
    [SerializeField] private Fueltank _fueltank;
    [SerializeField] private Namer _places;
    [SerializeField] private ButtonFuel _fuelerAdder;

    private void OnEnable()
    {
        _fuelerAdder.ButtonClicked += TryRefill;
    }

    private void OnDisable()
    {
        _fuelerAdder.ButtonClicked -= TryRefill;
    }

    public void Activate()
    {
        StartCoroutine(Activating());
    }

    public void Finish()
    {
        StartCoroutine(Deactivating());
    }

    public bool IsFuelEnough()
    {
        return _fueltank.CheckFull();
    }

    public Namer GetPlaces()
    {
        return _places.Get();
    }

    public Fueltank GetFueltank()
    {
        return _fueltank;
    }

    private void TryRefill()
    {
        if (_fueltank.GetEnoughValue() == 0)
            return;

        int coustRefull = _fueltank.GetEnoughValue() * FuelCoust;

        if (_game.TryPay(coustRefull))
        {
            _fueltank.Refill();
        }
        else
        {
            return;
        }
    }

    private IEnumerator Activating()
    {
        _animator.PlayStart();
        yield return new WaitForSeconds(3f);
        _blind.Open();
        _map.Activate();
        _ferryboatBackground.SetActive(true);
        _numberingText.Activate();
    }

    private IEnumerator Deactivating()
    {
        _fueltank.Travel();
        _map.Deactivate();
        _blind.Close();
        yield return new WaitForSeconds(1f);

        _animator.PlayFinish();
        _ferryboatBackground.SetActive(false);
        _numberingText.Deactivate();
    }

    public Map GetMap()
    {
        return _map;
    }
}
