using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Obstacle
{
    public class ObstacleExplosiveEffector : MonoBehaviour
    {
        [SerializeField] private ParticleSystemRenderer _explosiveEffect;
        [SerializeField] private ParticleSystemRenderer _changeViewCarEffect;
        [SerializeField] private ParticleSystemRenderer _changeViewCarNegativeEffect;

        private Queue<ParticleSystemRenderer> _explosiveEffects = new Queue<ParticleSystemRenderer>();
        private Queue<ParticleSystemRenderer> _changeViewCarEffects = new Queue<ParticleSystemRenderer>();
        private Queue<ParticleSystemRenderer> _changeViewCarNegativeEffects = new Queue<ParticleSystemRenderer>();

        private float _delayValue = 0.3f;
        private WaitForSeconds _delay;

        private void OnEnable()
        {
            _delay = new WaitForSeconds(_delayValue);

            if (_explosiveEffects.Count == 0)
                _explosiveEffects.Enqueue(_explosiveEffect);

            if (_changeViewCarEffects.Count == 0)
                _changeViewCarEffects.Enqueue(_changeViewCarEffect);

            if (_changeViewCarNegativeEffects.Count == 0)
                _changeViewCarNegativeEffects.Enqueue(_changeViewCarNegativeEffect);

            StartCoroutine(RemoveEffect(_changeViewCarEffects.Dequeue()));
            StartCoroutine(RemoveEffectExplosive(_explosiveEffects.Dequeue()));
            StartCoroutine(RemoveEffectNegative(_changeViewCarNegativeEffects.Dequeue()));
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

        public void ChangeViewCarNegativeEffects(Vector3 target)
        {
            ParticleSystemRenderer effect;

            if (_changeViewCarNegativeEffects.Count == 0)
            {
                _changeViewCarNegativeEffects.Enqueue(_changeViewCarNegativeEffect);
                effect = _changeViewCarNegativeEffects.Dequeue();
            }
            else
            {
                effect = _changeViewCarNegativeEffects.Dequeue();
            }

            effect.transform.position = target;
            effect.gameObject.SetActive(true);
            StartCoroutine(RemoveEffectNegative(effect));
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

        private IEnumerator RemoveEffectNegative(ParticleSystemRenderer effect)
        {
            yield return _delay;

            effect.gameObject.SetActive(false);
            _changeViewCarNegativeEffects.Enqueue(effect);
        }
    }
}