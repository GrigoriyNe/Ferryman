using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CarAnimator : MonoBehaviour
{
    private const string Wrong = "Wrong";
    private const string Turn = "Turn";

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void WrongAnimationStart()
    {
        _animator.SetTrigger(Wrong);
    }

    public void TurnAnimationStart()
    {
        _animator.SetTrigger(Turn);
    }
}