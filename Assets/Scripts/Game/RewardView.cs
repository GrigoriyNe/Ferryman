using System.Collections.Generic;
using UnityEngine;

public class RewardView : MonoBehaviour
{
    [SerializeField] private List<Sprite> _positiveCoins;
    [SerializeField] private List<Sprite> _negativeCoins;
    [SerializeField] private List<Sprite> _neturalCoins;

    public Sprite GetPositiveView(int value)
    {
        return _positiveCoins[Mathf.Abs(value - 1)];
    }

    public Sprite GetNegativeiveView(int value)
    {
        return _negativeCoins[Mathf.Abs( value + 1 )];
    }

    public Sprite GetNeturalView(int value)
    {
        return _neturalCoins[Mathf.Abs(value - 1)];
    }
}