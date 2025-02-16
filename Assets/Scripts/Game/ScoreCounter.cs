using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private Map _map;
    [SerializeField] private Game _game;

    private int _maxScore;

    public int Score { get; private set; }

    public void Activate()
    {
        gameObject.SetActive(true);
        Score = 0;
        _maxScore = _map.CountFinishPlace;
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
        Score = 0;
    }

    public void AddScore()
    {
        Score += 1;

        if (Score >= _maxScore)
        {
            _game.RoundOver();
        }
    }

    public void RemoveScore()
    {
        Score -= 1;
    }
}
