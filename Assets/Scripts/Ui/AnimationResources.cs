using UnityEngine;

public class AnimationResources : MonoBehaviour
{
    private const string Start = "Start";
    private const string IsPlay = "IsPlay";

    [SerializeField] private Animator _animatorMoney;
    [SerializeField] private Animator _animatorBombUi;

    public void ActivateMoney()
    {
        _animatorMoney.SetTrigger(Start);
    }

    public void ActivateBombUi()
    {
        _animatorBombUi.SetBool(IsPlay, true);
    }

    public void DeactivateBombUi()
    {
        _animatorBombUi.SetBool(IsPlay, false);
    }
}
