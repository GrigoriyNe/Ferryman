using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    private const int MultiplicationValue = 2;

    [SerializeField] private MapLogic _map;
    [SerializeField] private Game _game;
    [SerializeField] private ScoreSteps _step;
    [SerializeField] private RewardCounter _rewarder;

    public int MaxPossibleFinishPlaces { get; private set; }

    public int Score { get; private set; }

    public void Activate()
    {
        gameObject.SetActive(true);
        Score = 0;
        MaxPossibleFinishPlaces = (_map.CountFinishPlace + _map.CountFinishSpesialPlace) * MultiplicationValue;
        _step.SetStartValue(MaxPossibleFinishPlaces);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
        Score = 0;
    }

    public void AddScore(int reward)
    {
        _rewarder.ReckonCell(reward);
        Score += 1;

        if (Score == MaxPossibleFinishPlaces)
        {
            _game.RoundOver();
        }
    }

    public void RemoveScore(int reward)
    {
        _rewarder.ReckonCell(-reward);
        Score -= 1;
        _step.ChangeOnOne();
    }
}
