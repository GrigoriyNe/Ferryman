using System;
using System.Collections.Generic;
using UnityEngine;

public class CarMoverEffector : MonoBehaviour
{

    [SerializeField] private ParticleSystemRenderer _smokeEffect;
    [SerializeField] private ParticleSystemRenderer _trailEffectVertical;
    //[SerializeField] private ParticleSystemRenderer _trailEffectHorizontal;


    private Queue<ParticleSystemRenderer> _smokeEffects = new Queue<ParticleSystemRenderer> ();
    private Queue<ParticleSystemRenderer> _trailsVertical = new Queue<ParticleSystemRenderer> ();
  //  private Queue<ParticleSystemRenderer> _trailsHorizontal= new Queue<ParticleSystemRenderer> ();

    private void OnEnable()
    {
        if (_smokeEffects.Count < 1)
            _smokeEffects.Enqueue(_smokeEffect);

        if (_trailsVertical.Count < 1)
            _trailsVertical.Enqueue(_trailEffectVertical);

        //if (_trailsHorizontal.Count < 1)
        //    _trailsHorizontal.Enqueue(_trailEffectHorizontal);
    }


    public ParticleSystemRenderer PlaySmoke()
    {
        ParticleSystemRenderer cash = _smokeEffects.Dequeue();
        _smokeEffects.Enqueue (cash);
        cash.gameObject.SetActive (true);  

        return cash;
    }

    public ParticleSystemRenderer PlayTrailVertical()
    {
        ParticleSystemRenderer cash = _trailsVertical.Dequeue();
        _trailsVertical.Enqueue(cash);
        cash.gameObject.SetActive(true);

        return cash;
    }
}