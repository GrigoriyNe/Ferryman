using UnityEngine;
using YG.Utils.LB;
using YG;

public class LeaderbordCounter : MonoBehaviour
{
    private const string TechnoName = "Money";

    [SerializeField] private Wallet _wallet;
    [SerializeField] private LeaderboardYG _board;

    private long _scorePlayer;

    private void OnEnable()
    {
        YandexGame.onGetLeaderboard += OnGetLeaderboards;
        YandexGame.GetLeaderboard(TechnoName, 7, 3, 3, "Small");
    }

    private void OnDisable()
    {
        YandexGame.onGetLeaderboard -= OnGetLeaderboards;
    }

    public void ChangeCounter()
    {
        if (_scorePlayer < _wallet.Money)
        {
            _scorePlayer = _wallet.Money;
            _board.NewScore(_scorePlayer);
            _board.UpdateLB();
        }
    }

    private void OnGetLeaderboards(LBData lb)
    {
        if (lb.technoName == TechnoName)
            _scorePlayer = lb.thisPlayer.score;
    }
}