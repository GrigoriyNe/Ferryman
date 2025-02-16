using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    private const int RewardOnPanking = 10;
    private const int RewardOnSpescialPanking = 20;

    [SerializeField] private Map _map;
    [SerializeField] private Game _game;
    [SerializeField] private Wallet _walet;

    private int _maxScore;

    public int Score { get; private set; }

    public void Activate()
    {
        gameObject.SetActive(true);
        Score = 0;
        _maxScore = _map.CountFinishPlace + _map.CountFinishSpesialPlace;
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
        Score = 0;
    }

    public void AddScore()
    {
        Score += 1;

        if (Score == _maxScore)
        {
            _walet.AddMoney(Score * RewardOnPanking + (_map.CountFinishSpesialPlace * RewardOnSpescialPanking));
            _game.RoundOver();
        }
    }

    public void RemoveScore()
    {
        Score -= 1;
    }
}
