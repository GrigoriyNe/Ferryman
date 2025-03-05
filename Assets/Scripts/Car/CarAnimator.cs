using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CarAnimator : MonoBehaviour
{
    private const string Wrong = "Wrong";

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void WrongAnimationStart()
    {
        _animator.SetTrigger(Wrong);
    }
}