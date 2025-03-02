using UnityEngine;

public class AnimationResources : MonoBehaviour
{
    private const string Start = "Start";

    [SerializeField] private Animator _animatorMoney;
    [SerializeField] private Animator _animatorDollars;

    public void ActivateMoney()
    {
        _animatorMoney.SetTrigger(Start);
    }

    public void ActivateDollars()
    {
        _animatorDollars.SetTrigger(Start);
    }
}
