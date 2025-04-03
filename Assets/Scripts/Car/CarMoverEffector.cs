using System;
using System.Collections.Generic;
using UnityEngine;

public class CarMoverEffector : MonoBehaviour
{

    [SerializeField] private ParticleSystemRenderer _smokeEffect;
    [SerializeField] private Soungs _sougs;


    private Queue<ParticleSystemRenderer> _smokeEffects = new Queue<ParticleSystemRenderer> ();

    private void OnEnable()
    {
        if (_smokeEffects.Count < 1)
            _smokeEffects.Enqueue(_smokeEffect);
    }

    public ParticleSystemRenderer PlayMoveEffects()
    {
        ParticleSystemRenderer cash = _smokeEffects.Dequeue();
        _smokeEffects.Enqueue (cash);
        cash.gameObject.SetActive (true);
        PlaySoungMove();

        return cash;
    }

    public void PlayMoveQuenue()
    {
        _sougs.PlayQuenueMovingCars();
    }

    private void PlaySoungMove()
    {
        _sougs.PlayMovedSoung();
    }
}