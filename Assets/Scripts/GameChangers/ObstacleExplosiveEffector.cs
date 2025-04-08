using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleExplosiveEffector : MonoBehaviour
{
    [SerializeField] private ParticleSystemRenderer _explosiveEffect;
    [SerializeField] private ParticleSystemRenderer _changeViewCarEffect;

    private Queue<ParticleSystemRenderer> _explosiveEffects = new Queue<ParticleSystemRenderer>();
    private Queue<ParticleSystemRenderer> _changeViewCarEffects = new Queue<ParticleSystemRenderer>();

    private float _delayValue = 0.3f;
    private WaitForSeconds _delay;

    private void OnEnable()
    {
        _delay = new WaitForSeconds(_delayValue);

        if (_explosiveEffects.Count == 0)
            _explosiveEffects.Enqueue(_explosiveEffect);

        if (_changeViewCarEffects.Count == 0)
            _changeViewCarEffects.Enqueue(_changeViewCarEffect);

        StartCoroutine(RemoveEffect(_changeViewCarEffects.Dequeue()));
    }

    public void ExplosiveEffects(Vector3 target)
    {
        ParticleSystemRenderer effect;

        if (_explosiveEffects.Count == 0)
        {
            _explosiveEffects.Enqueue(_explosiveEffect);
            effect = _explosiveEffects.Dequeue();
        }
        else
        {
            effect = _explosiveEffects.Dequeue();
        }

        effect.transform.position = target;
        effect.gameObject.SetActive(true);
        StartCoroutine(RemoveEffectExplosive(effect));
    }

    public void ChangeViewCarEffects(Vector3 target)
    {
        ParticleSystemRenderer effect;

        if (_changeViewCarEffects.Count == 0)
        {
            _changeViewCarEffects.Enqueue(_changeViewCarEffect);
            effect = _changeViewCarEffects.Dequeue();
        }
        else
        {
            effect = _changeViewCarEffects.Dequeue();
        }

        effect.transform.position = target;
        effect.gameObject.SetActive(true);
        StartCoroutine(RemoveEffect(effect));
    }

    private IEnumerator RemoveEffectExplosive(ParticleSystemRenderer effect)
    {
        yield return _delay;

        effect.gameObject.SetActive(false);
        _explosiveEffects.Enqueue(effect);
    }

    private IEnumerator RemoveEffect(ParticleSystemRenderer effect)
    {
        yield return _delay;

        effect.gameObject.SetActive(false);
        _changeViewCarEffects.Enqueue(effect);
    }
}