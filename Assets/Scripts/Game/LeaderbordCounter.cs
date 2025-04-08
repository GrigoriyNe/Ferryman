using UnityEngine;
using YG.Utils.LB;
using YG;
using Unity.VisualScripting;

public class LeaderbordCounter : MonoBehaviour
{
    private const string TechnoName = "MaxMoney";

    [SerializeField] private Wallet _wallet;
    [SerializeField] private LeaderboardYG _board;

    private long _scorePlayer;

    private void OnEnable()
    {
        YandexGame.GetLeaderboard(TechnoName, 6, 6, 6, "small");
        YandexGame.onGetLeaderboard += OnGetLeaderboards;
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
        }
    }

    private void OnGetLeaderboards(LBData lb)
    {
        if (lb.thisPlayer != null)
            _scorePlayer = lb.thisPlayer.score;
    }
}