using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarGroup
{
    public class CarMoverEffector : MonoBehaviour
    {
        [SerializeField] private ParticleSystemRenderer _smokeEffect;
        [SerializeField] private SoungsGroup.Soungs _soungs;
        [SerializeField] private Obstacle.ObstacleExplosiveEffector _effectorObstacle;

        private Queue<ParticleSystemRenderer> _smokeEffects = new Queue<ParticleSystemRenderer>();

        private WaitForSeconds _waitRemove;
        private float _waitTime = 3;

        private void Start()
        {
            _waitRemove = new WaitForSeconds(_waitTime);
        }

        public void PlayMoveQuenue()
        {
            _soungs.PlayQuenueMovingCars();
        }

        public void PlayMoveEffects(Transform targetTransform)
        {
            ParticleSystemRenderer effect;
            Vector3 target = targetTransform.position;

            if (_smokeEffects.Count == 0)
                _smokeEffects.Enqueue(_smokeEffect);

            effect = _smokeEffects.Dequeue();
            effect.gameObject.SetActive(true);
            effect.transform.SetParent(targetTransform);
            effect.transform.position = target;
            StartCoroutine(RemoveEffect(effect));

            PlaySoungMove();
        }

        public void PlayFinishEffects(Vector3 target)
        {
            _effectorObstacle.ChangeViewCarEffects(target);
        }

        public void PlayFinishNegativeEffects(Vector3 target)
        {
            _effectorObstacle.ChangeViewCarNegativeEffects(target);
        }

        private IEnumerator RemoveEffect(ParticleSystemRenderer effect)
        {
            yield return _waitRemove;

            effect.transform.SetParent(transform);
            effect.gameObject.SetActive(false);
            _smokeEffects.Enqueue(effect);
        }

        private void PlaySoungMove()
        {
            _soungs.PlayMovedSoung();
        }
    }
}