using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RewardCounter : MonoBehaviour
{
    private const int Offset = 1;
    private const int LowerPositive = 1;
    private const int MaxPositive = 3 + Offset;
    private const int LowerNegative = -3;
    private const int MaxNegative = -1 + Offset;

    private const int LowerPositiveSpesial = 4;
    private const int MaxPositiveSpesial = 6 + Offset;
    private const int LowerNegativeSpesial = -6;
    private const int MaxNegativeSpesial = -4 + Offset;

    [SerializeField] private MapLogic _map;
    [SerializeField] private TextMeshProUGUI _textWonCell;
    [SerializeField] private ScoreSteps _stepCounter;
    [SerializeField] private AnimationResources _animation;
    [SerializeField] private Soungs _soungs;

    private List<TileHelper> _emptyTiles = new List<TileHelper>();
    private List<TileHelper> _emptySpesialTiles = new List<TileHelper>();

    private List<TileHelper> _filledFinishTiles = new List<TileHelper>();
    private List<TileHelper> _filledFinishSpesialTile = new List<TileHelper>();

    private List<int> _cellPositiveEffect = new List<int>();
    private List<int> _cellNegativEffect = new List<int>();
    private List<int> _cellSpesialPositiveEffect = new List<int>();
    private List<int> _cellSpesialNegativeEffect = new List<int>();

    private int _wonValue = 0;

    private void OnDisable()
    {
        _emptyTiles = new List<TileHelper>();
        _emptySpesialTiles = new List<TileHelper>();
        _filledFinishTiles = new List<TileHelper>();
        _filledFinishSpesialTile = new List<TileHelper>();
        _wonValue = 0;
        _textWonCell.text = string.Empty;
    }

    public int GetRewardValue()
    {
        int resultNegative = 0;
        int result = 0;

        foreach (TileHelper tileHelper in _filledFinishTiles)
            if (tileHelper.Reward < 0)
                resultNegative += tileHelper.Reward;

        foreach (TileHelper tileHelper in _filledFinishSpesialTile)
            if (tileHelper.Reward < 0)
                resultNegative += tileHelper.Reward;

        result = _wonValue + resultNegative;

        return result;
    }

    public void SetStartPositions(List<TileHelper> startPositions)
    {
        foreach (TileHelper tile in startPositions)
        {
            if (tile.gameObject.activeSelf)
            {
                if (_map.CheckObstacle(tile.CordX, tile.CordY) == false)
                {
                    _emptyTiles.Add(tile);
                }
                else
                {
                    _filledFinishTiles.Add(tile);
                }
            }
        }
    }

    public void SetStartSpesialPositions(List<TileHelper> startSpesialPositions)
    {
        foreach (TileHelper tile in startSpesialPositions)
        {
            if (tile.gameObject.activeSelf)
            {
                if (_map.CheckObstacle(tile.CordX, tile.CordY) == false)
                {
                    _emptySpesialTiles.Add(tile);
                }
                else
                {
                    _filledFinishSpesialTile.Add(tile);
                }
            }
        }
    }

    public void WriteReward()
    {
        SetCellListEffects();

        foreach (TileHelper tile in _emptyTiles)
        {
            if (tile.gameObject.activeSelf)
            {
                int reward = _cellPositiveEffect[Random.Range(0, _cellPositiveEffect.Count)];
                tile.SetRewardValue(reward);
            }
        }

        foreach (TileHelper tile in _emptySpesialTiles)
        {
            if (tile.gameObject.activeSelf)
            {
                int reward = _cellSpesialPositiveEffect[Random.Range(0, _cellSpesialPositiveEffect.Count)];
                tile.SetRewardValue(reward);
            }
        }

        foreach (TileHelper tile in _filledFinishTiles)
        {
            if (tile.gameObject.activeSelf)
            {
                int reward = _cellNegativEffect[Random.Range(0, _cellNegativEffect.Count)];
                tile.SetRewardValue(reward);
            }
        }

        foreach (TileHelper tile in _filledFinishSpesialTile)
        {
            if (tile.gameObject.activeSelf)
            {
                int reward = _cellSpesialNegativeEffect[Random.Range(0, _cellSpesialNegativeEffect.Count)];
                tile.SetRewardValue(reward);
            }
        }

        _textWonCell.text = GetRewardValue().ToString();
    }

    public void ReckonCell(int tileReward)
    {
        _wonValue += tileReward;
        _textWonCell.text = GetRewardValue().ToString();

        if (tileReward > 0)
        {
            AnimatedChanged();
            _soungs.PlayCoinPositiveSoung();
        }
        else
        {
            AnimatedNegativeChanged();
            _soungs.PlayCoinNegativeSoung();
        }
    }

    public void ChangeRewardCell(TileHelper tile)
    {
        tile.SetRewardValue(_cellPositiveEffect[Random.Range(0, _cellPositiveEffect.Count)]);
        _textWonCell.text = GetRewardValue().ToString();
        AnimatedChanged();
    }

    public void ChangeRewardSpesialCell(TileHelper tile)
    {
        tile.SetRewardValue(_cellSpesialPositiveEffect[Random.Range(0, _cellSpesialPositiveEffect.Count)]);
        _textWonCell.text = GetRewardValue().ToString();
        AnimatedChanged();
    }

    private void SetCellListEffects()
    {
        for (int i = LowerPositive; i < MaxPositive; i++)
        {
            _cellPositiveEffect.Add(i);
        }

        for (int i = LowerNegative; i < MaxNegative; i++)
        {
            _cellNegativEffect.Add(i);
        }

        for (int i = LowerPositiveSpesial; i < MaxPositiveSpesial; i++)
        {
            _cellSpesialPositiveEffect.Add(i);
        }

        for (int i = LowerNegativeSpesial; i < MaxNegativeSpesial; i++)
        {
            _cellSpesialNegativeEffect.Add(i);
        }
    }

    private void AnimatedChanged()
    {
        _animation.ActivateAnimatorRestart();
    }

    private void AnimatedNegativeChanged()
    {
        _animation.ActivateAnimatorNegativeRestart();
    }
}
