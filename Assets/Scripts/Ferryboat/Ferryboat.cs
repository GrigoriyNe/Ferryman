using System.Collections;
using UnityEngine;

public class Ferryboat : MonoBehaviour
{
    [SerializeField] private Game _game;
    [SerializeField] private Map _map;
    [SerializeField] private NumberingPlaceText _numberingText;
    [SerializeField] private WindowBlind _blind;
    [SerializeField] private FerryboatAnimator _animator;
    [SerializeField] private Namer _places;

    public void Activate()
    {
        StartCoroutine(Activating());
    }

    public void Finish()
    {
        StartCoroutine(Deactivating());
    }

    public Namer GetPlaces()
    {
        return _places.Get();
    }

    private IEnumerator Activating()
    {
        _animator.PlayStart();
        yield return new WaitForSeconds(3f);
        _blind.Open();
        _map.Activate();
        _numberingText.Activate();
    }

    private IEnumerator Deactivating()
    {
        _map.Deactivate();
        _blind.Close();
        yield return new WaitForSeconds(1f);

        _animator.PlayFinish();
        _numberingText.Deactivate();
    }

    public Map GetMap()
    {
        return _map;
    }
}
