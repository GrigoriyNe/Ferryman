using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AnimationResources : MonoBehaviour
{
    private const string Start = "Start";
    private const string StartNegative = "StartNegative";
    private const string IsPlay = "IsPlay";

    [SerializeField] private Animator _animatorMoney;
    [SerializeField] private Animator _animatorBombUi;
    [SerializeField] private Animator _animatorStepUi;
    [SerializeField] private Animator _animatorLeveUi;
    [SerializeField] private Animator _animatorRestart;
    [SerializeField] private ScoreSteps _scoreCounter;
    [SerializeField] private Button _restartButton;

    private Coroutine _deativating = null;
    private float _stepDeativatingValue = 0.5f;
    private WaitForSeconds _stepDeativatingWaing;
    private float _uiDeativatingValue = 1f;
    private WaitForSeconds _UiDeativatingWaing;

    private void Awake()
    {
        _UiDeativatingWaing = new WaitForSeconds(_uiDeativatingValue);
        _stepDeativatingWaing = new WaitForSeconds(_stepDeativatingValue);
    }

    public void ActivateRestartButtomAnimatoin()
    {
        _animatorRestart.SetBool(Start, true);
        _animatorStepUi.SetBool(Start, true);
    }

    public void DeactivateRestartButtomAnimatoin()
    {
        StartCoroutine(DeactivateAnimation(_animatorRestart, _UiDeativatingWaing));
        _deativating = StartCoroutine(DeactivateAnimation(_animatorStepUi, _stepDeativatingWaing));
    }

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

    public void ActivateStep()
    {
        if (_deativating == null)
        {
            _animatorStepUi.SetBool(Start, true);
            _deativating = StartCoroutine(DeactivateAnimation(_animatorStepUi, _stepDeativatingWaing));
        }
        else
        {
            _deativating = null;
            _animatorStepUi.SetBool(Start, false);
            _animatorStepUi.SetBool(Start, true);
            _deativating = StartCoroutine(DeactivateAnimation(_animatorStepUi, _stepDeativatingWaing));
        }
    }

    public void ActivateLevelUi()
    {
        _animatorLeveUi.SetBool(Start, true);

        StartCoroutine(DeactivateAnimation(_animatorLeveUi, _UiDeativatingWaing));
    }

    public void ActivateAnimatorRestart()
    {
        _animatorRestart.SetBool(Start, true);

        StartCoroutine(DeactivateAnimation(_animatorRestart, _UiDeativatingWaing));
    }

    public void ActivateAnimatorNegativeRestart()
    {
        _animatorRestart.SetBool(StartNegative, true);

        StartCoroutine(DeactivateAnimationNegative(_animatorRestart, _UiDeativatingWaing));
    }

    private IEnumerator DeactivateAnimation(Animator animatorUi, WaitForSeconds waiting)
    {
        yield return waiting;

        animatorUi.SetBool(Start, false);
    }

    private IEnumerator DeactivateAnimationNegative(Animator animatorUi, WaitForSeconds waiting)
    {
        yield return waiting;

        animatorUi.SetBool(StartNegative, false);
    }
}
