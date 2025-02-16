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

    public int CountFinishPlace => _map.CountFinishPlace;
    public int CountSpesialFinishPlace => _map.CountFinishSpesialPlace;

    public void Activate()
    {
        _fueltank.Travel();
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
        _map.Deactivate();
        _blind.Close();
        yield return new WaitForSeconds(1f);

        _animator.PlayFinish();
        _ferryboatBackground.SetActive(false);
        _numberingText.Deactivate();
    }
}
