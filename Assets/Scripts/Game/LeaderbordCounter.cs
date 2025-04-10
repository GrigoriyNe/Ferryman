using UnityEngine;
using YG;
using YG.Utils.LB;



    public class LeaderbordCounter : MonoBehaviour
{
    private const string TechnoName = "Money";

    [SerializeField] private Wallet _wallet;
    [SerializeField] private LeaderboardYG _board;

    private int _scorePlayer;

    private void OnEnable()
    {
        YG2.onGetLeaderboard += OnGetLeaderboards;
      //  YG2.GetLeaderboard(TechnoName, 7, 3, 3, "Small");
    }

    private void OnDisable()
    {
        YG2.onGetLeaderboard -= OnGetLeaderboards;
    }

    public void ChangeCounter()
    {
        if (_scorePlayer < _wallet.Money)
        {
            _scorePlayer = _wallet.Money;
            _board.SetLeaderboard(_scorePlayer);
            _board.UpdateLB();
        }
    }

    private void OnGetLeaderboards(LBData lb)
    {
        if (lb.technoName == TechnoName)
            if(lb.currentPlayer != null)
            _scorePlayer = lb.currentPlayer.score;
    }
}
