using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class ViewResources : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moneyCount;
    [SerializeField] private TextMeshProUGUI _bombCount;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private AnimationResources _animator;
    [SerializeField] private ObstacleLogic _obstacleLogic;

    private int _bombValue = 2;

    private void OnEnable()
    {
        _wallet.ChangedMoney += OnChangedMoney;
        _wallet.ChangedBomb += OnChangedCountBomb;
        _obstacleLogic.BombUsed += OnDeactivateBomb;
        _wallet.BombUse += OnActivatedBomb;
    }

    private void OnDisable()
    {
        _wallet.ChangedMoney -= OnChangedMoney;
        _wallet.ChangedBomb -= OnChangedCountBomb;
        _obstacleLogic.BombUsed -= OnDeactivateBomb;
        _wallet.BombUse -= OnActivatedBomb;
    }

    private void OnChangedCountBomb(int value)
    {
        _bombValue = value;
        _bombCount.text = _bombValue.ToString();
    }

    private void OnActivatedBomb()
    {
        _animator.ActivateBombUi();
    }

    private void OnDeactivateBomb()
    {
        _bombCount.text = _bombValue.ToString();
        _animator.DeactivateBombUi();
    }

    private void OnChangedMoney(int value)
    {
        _moneyCount.text = value.ToString();
        _animator.ActivateMoney();
    }
}
