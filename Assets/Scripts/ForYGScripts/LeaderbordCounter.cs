using UnityEngine;
using YG.Utils.LB;

namespace YG
{
    public class LeaderbordCounter : MonoBehaviour
    {
        private const string TechnoName = "Money";

        [SerializeField] private PlayerResouce.Wallet _wallet;
        [SerializeField] private LeaderboardYG _board;

        private int _scorePlayer;

        private void OnEnable()
        {
            YG2.onGetLeaderboard += OnGetLeaderboards;
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
                if (lb.currentPlayer != null)
                    _scorePlayer = lb.currentPlayer.score;
        }
    }
}