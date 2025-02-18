using System;
using System.Collections;
using UnityEngine;

public class Ferryboat : MonoBehaviour
{
    [SerializeField] private Map _map;
    [SerializeField] private NumberingPlaceText _numberingText;
    [SerializeField] private GameObject _ferryboatBackground;
    [SerializeField] private WindowBlind _blind;
    [SerializeField] private FerryboatAnimator _animator;
    [SerializeField] private Fueltank _fueltank;
    [SerializeField] private NamesOfParkingPlaces _places;

    public void Activate()
    {
        _animator.PlayStart();
        StartCoroutine(Activating());
    }

    public void Finish()
    {
        StartCoroutine(Deactivating());
    }

    public bool CheckFuel()
    {
        return _fueltank.CheckFull();
    }

    public int GetEnoughValue()
    {
        return _fueltank.GetEnoughValue();
    }

    public void Refill()
    {
        _fueltank.Refill();
    }
    public NamesOfParkingPlaces GetPlaces()
    {
        return _places;
    }

    private IEnumerator Activating()
    {
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
}
