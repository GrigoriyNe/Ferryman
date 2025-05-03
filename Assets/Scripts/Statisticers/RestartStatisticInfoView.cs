using TMPro;
using UnityEngine;

namespace Statisticers
{
    public class RestartStatisticInfoView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _levelCount;
        [SerializeField] private TextMeshProUGUI _maxMoneyCount;
        [SerializeField] private TextMeshProUGUI _usedBomb;
        [SerializeField] private RestartStatisticInfoCounter _counter;

        private void OnEnable()
        {
            _levelCount.text = _counter.GetLevelInfo();
            _maxMoneyCount.text = _counter.GetWorkedMoneyInfo();
            _usedBomb.text = _counter.GetUsedBombInfo();
        }
    }
}