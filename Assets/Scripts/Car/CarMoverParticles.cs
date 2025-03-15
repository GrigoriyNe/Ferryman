using System;
using System.Collections.Generic;
using UnityEngine;

public class CarMoverParticles : MonoBehaviour
{

    [SerializeField] private ParticleSystemRenderer _smokeEffect;
    [SerializeField] private ParticleSystemRenderer _trailEffect;


    private Queue<ParticleSystemRenderer> _smokeEffects = new Queue<ParticleSystemRenderer> ();
    private Queue<ParticleSystemRenderer> _trailEffects = new Queue<ParticleSystemRenderer> ();

    private void OnEnable()
    {
        if (_smokeEffects.Count < 1)
        {
            _smokeEffects.Enqueue(_smokeEffect);
        }

        if (_trailEffects.Count < 1)
        {
            _trailEffects.Enqueue(_trailEffect);
        }
    }


    public ParticleSystemRenderer Play()
    {
        ParticleSystemRenderer cash = _smokeEffects.Dequeue();
        _smokeEffects.Enqueue (cash);
        cash.gameObject.SetActive (true);  

        return cash;
    }

    public ParticleSystemRenderer PlayTrail()
    {
        ParticleSystemRenderer cash = _trailEffects.Dequeue();
        _trailEffects.Enqueue(cash);
        cash.gameObject.SetActive(true);

        return cash;
    }
}