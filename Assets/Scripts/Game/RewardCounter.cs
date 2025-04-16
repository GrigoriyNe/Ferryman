using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RewardCounter : MonoBehaviour
{
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
        _textWonCell.text = "";
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
                if (_map.CheckObstacle(tile.cordX, tile.cordY) == false)
                    _emptyTiles.Add(tile);
                else
                    _filledFinishTiles.Add(tile);
        }
    }

    public void SetStartSpesialPositions(List<TileHelper> startSpesialPositions)
    {
        foreach (TileHelper tile in startSpesialPositions)
        {
            if (tile.gameObject.activeSelf)
                if (_map.CheckObstacle(tile.cordX, tile.cordY) == false)
                    _emptySpesialTiles.Add(tile);
                else
                    _filledFinishSpesialTile.Add(tile);
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
        _cellPositiveEffect.Add(1);
        _cellPositiveEffect.Add(2);
        _cellPositiveEffect.Add(3);

        _cellNegativEffect.Add(-1);
        _cellNegativEffect.Add(-2);
        _cellNegativEffect.Add(-3);

        _cellSpesialPositiveEffect.Add(4);
        _cellSpesialPositiveEffect.Add(5);
        _cellSpesialPositiveEffect.Add(6);

        _cellSpesialNegativeEffect.Add(-4);
        _cellSpesialNegativeEffect.Add(-5);
        _cellSpesialNegativeEffect.Add(-6);
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
