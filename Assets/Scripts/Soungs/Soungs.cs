using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soungs : MonoBehaviour
{
    [SerializeField] private List<AudioSource> _movedCarSoungs;
    [SerializeField] private List<AudioSource> _bombEffect;
    [SerializeField] private List<AudioSource> _quenueMoveCarSoungs;
    [SerializeField] private AudioSource _clickSoung;
    [SerializeField] private AudioSource _restartSoung;
    [SerializeField] private AudioSource _ferryboatEngineSoung;
    [SerializeField] private AudioSource _garageSoung;
    [SerializeField] private AudioSource _startCreateCars;

    public void PlayMovedSoung()
    {
        _movedCarSoungs[Random.Range(0, _movedCarSoungs.Count)].Play();
    }

    public void PlayBombSoung()
    {
        _bombEffect[Random.Range(0, _bombEffect.Count)].Play();
    }

    public void PlayClickSoung()
    {
        _clickSoung.Play();
    }

    public void PlayRestartSoung()
    {
        _restartSoung.Play();
    }

    public void PlayFerryboatEngineSoung()
    {
        _ferryboatEngineSoung.Play();
    }

    public void PlayGarageSoung()
    {
        _garageSoung.Play();
    }

    public void PlayQuenueMovingCars()
    {
        _quenueMoveCarSoungs[Random.Range(0, _quenueMoveCarSoungs.Count)].Play();
    }

    internal void PlayCreatCar()
    {
        _startCreateCars.Play();
    }
}

