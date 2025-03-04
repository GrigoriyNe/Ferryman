using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    private const int RewardOnPanking = 10;
    private const int RewardOnSpescialPanking = 20;
    private const int StepMultiplayer = 3;

    [SerializeField] private MapLogic _map;
    [SerializeField] private Game _game;
    [SerializeField] private Wallet _walet;
    [SerializeField] private ScoreSteps _step;
    [SerializeField] private RestartButtonActivator _restart;

    public int MaxPossibleFinishPlaces { get; private set; }

    public int Score { get; private set; }

    public void Activate()
    {
        gameObject.SetActive(true);
        Score = 0;
        MaxPossibleFinishPlaces = 0;

    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
        Score = 0;
        _restart.Deactivate();
    }

    public void AddMaxScore(int value)
    {
        MaxPossibleFinishPlaces += value;
        _step.SetStartValue(StepMultiplayer * MaxPossibleFinishPlaces);
    }

    public void AddScore()
    {
        Score += 1;
        _step.ChangeOn(1);

        if (_restart.gameObject.activeSelf == false)
            if (Score > MaxPossibleFinishPlaces / 2)
                _restart.Activate();

        if (Score == MaxPossibleFinishPlaces)
        {
            _walet.AddMoney(Score * RewardOnPanking + (_map.CountFinishSpesialPlace * RewardOnSpescialPanking));
            _game.RoundOver();
        }
    }

    public void RemoveScore()
    {
        Score -= 1;
        _step.ChangeOn(1);
    }
}
