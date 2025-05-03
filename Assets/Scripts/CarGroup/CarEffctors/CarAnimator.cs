using System.Collections;
using UnityEngine;

namespace CarGroup
{
    [RequireComponent(typeof(Animator))]
    public class CarAnimator : MonoBehaviour
    {
        private const string Wrong = "Wrong";
        private const string Turn = "Turn";

        private Animator _animator;
        private float _delayValue = 2f;
        private WaitForSeconds _delay;

        private void Start()
        {
            _delay = new WaitForSeconds(_delayValue);
            _animator = GetComponent<Animator>();
        }

        public void WrongAnimationStart()
        {
            _animator.SetBool(Wrong, true);
            StartCoroutine(Deactivated());
        }

        public void TurnAnimationStart()
        {
            _animator.SetBool(Turn, true);
            StartCoroutine(DeactivatedTurn());
        }

        private IEnumerator Deactivated()
        {
            yield return _delay;
            _animator.SetBool(Wrong, false);
        }

        private IEnumerator DeactivatedTurn()
        {
            yield return _delay;
            _animator.SetBool(Turn, false);
        }
    }
}