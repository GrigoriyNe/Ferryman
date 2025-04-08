using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    private const int MultiplicationValue = 2;

    [SerializeField] private MapLogic _map;
    [SerializeField] private Game _game;
    [SerializeField] private ScoreSteps _step;
    [SerializeField] private RewardCounter _rewarder;

    public int MaxPossibleFinishPlaces { get; private set; }
    public int ScoreLess => _step.StepsLeft;

    public void Activate()
    {
        gameObject.SetActive(true);
        MaxPossibleFinishPlaces = _map.GetMaxPlaceCount() * MultiplicationValue;
        _step.SetStartValue(MaxPossibleFinishPlaces);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
        _step.SetStartValue(0);
    }

    public void AddScore(int reward)
    {
        _rewarder.ReckonCell(reward);
    }

    public void RemoveScore(int reward)
    {
        _rewarder.ReckonCell(-reward);
    }

    public void RemoveStep()
    {
        _step.ChangeOnOne();
    }
}
