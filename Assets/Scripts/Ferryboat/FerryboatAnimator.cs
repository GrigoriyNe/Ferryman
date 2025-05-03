using UnityEngine;

namespace FerryboatGroup
{
    public class FerryboatAnimator : MonoBehaviour
    {
        private const string Start = nameof(Start);
        private const string Finish = nameof(Finish);

        [SerializeField] private Animator _animator;

        public void PlayStart()
        {
            _animator.SetTrigger(Start);
        }

        public void PlayFinish()
        {
            _animator.SetTrigger(Finish);
        }
    }
}