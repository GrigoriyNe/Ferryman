using System;
using System.Collections;
using UnityEngine;

public class ScoreSteps : MonoBehaviour
{
    [SerializeField] private Game _game;

    private float _delayValue = 3f;
    private WaitForSeconds _delay;

    public event Action<int> Changed;

    public int StepsLeft { get; private set; }

    private void OnEnable()
    {
        _delay = new WaitForSeconds(_delayValue);
    }

    private void OnDisable()
    {
        StepsLeft = 0;
        Changed?.Invoke(StepsLeft);
    }

    public void SetStartValue(int value)
    {
        StepsLeft = value;
        Changed?.Invoke(StepsLeft - 1);
    }

    public void ChangeOnOne()
    {
        StepsLeft -= 1;
        Changed?.Invoke(StepsLeft - 1);

        if (StepsLeft == 1)
            StartCoroutine(EndingRound());
    }

    private IEnumerator EndingRound()
    {
        yield return _delay;

        _game.OfferRoundOver();
    }
}