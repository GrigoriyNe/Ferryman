using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RewardCounter : MonoBehaviour
{
    [SerializeField] private MapLogic _map;
    [SerializeField] private TextMeshProUGUI _textWonCell;

    private List<TileHelper> _startPositions = new List<TileHelper>();
    private List<TileHelper> _startSpesialPositions = new List<TileHelper>();

    private List<TileHelper> _filledStartPositions = new List<TileHelper>();
    private List<TileHelper> _filledStartSpesialPositions = new List<TileHelper>();

    private List<int> _cellPositiveEffect = new List<int>();
    private List<int> _cellNegativEffect = new List<int>();
    private List<int> _cellSpesialPositiveEffect = new List<int>();
    private List<int> _cellSpesialNegativeEffect = new List<int>();

    private int _wonValue = 0;

    public int GetRewardValue()
    {
        int resultNegative = 0;

        foreach (TileHelper tileHelper in _filledStartPositions)
            resultNegative += tileHelper.Reward;

        foreach (TileHelper tileHelper in _filledStartSpesialPositions)
            resultNegative += tileHelper.Reward;

        return _wonValue + resultNegative;
    }
    private void OnDisable()
    {
        _startPositions.Clear();
        _startSpesialPositions.Clear();
        _filledStartPositions.Clear();
        _filledStartSpesialPositions.Clear();
        _wonValue = 0;
    }

    public void SetStartPositions(List<TileHelper> startPositions)
    {
        foreach (TileHelper tile in startPositions)
        {
            if (_map.CheckObstacle(tile.cordX, tile.cordY) == false)
                _startPositions.Add(tile);
            else
                _filledStartPositions.Add(tile);
        }
    }

    public void SetStartSpesialPositions(List<TileHelper> startSpesialPositions)
    {
        foreach (TileHelper tile in startSpesialPositions)
        {
            if (_map.CheckObstacle(tile.cordX, tile.cordY) == false)
                _startSpesialPositions.Add(tile);
            else
                _filledStartSpesialPositions.Add(tile);
        }
    }

    public void WriteReward()
    {
        SetCellListEffects();

        foreach (TileHelper tile in _startPositions)
        {
            int reward = _cellPositiveEffect[Random.Range(0, _cellPositiveEffect.Count)];
            tile.SetRewardValue(reward);
        }

        foreach (TileHelper tile in _startSpesialPositions)
        {
            int reward = _cellSpesialPositiveEffect[Random.Range(0, _cellSpesialPositiveEffect.Count)];
            tile.SetRewardValue(reward);
        }

        foreach (TileHelper tile in _filledStartPositions)
        {
            int reward = _cellNegativEffect[Random.Range(0, _cellNegativEffect.Count)];
            tile.SetRewardValue(reward);
        }

        foreach (TileHelper tile in _filledStartSpesialPositions)
        {
            int reward = _cellSpesialNegativeEffect[Random.Range(0, _cellSpesialNegativeEffect.Count)];
            tile.SetRewardValue(reward);
        }

        _textWonCell.text = GetRewardValue().ToString();
    }

    public void ReckonCell(int tileReward)
    {
        _wonValue += tileReward;
        _textWonCell.text = GetRewardValue().ToString();
    }

    public void ChangeRewardCell(TileHelper tile)
    {
        tile.SetRewardValue(_cellPositiveEffect[Random.Range(0, _cellPositiveEffect.Count)]);
        _textWonCell.text = GetRewardValue().ToString();
    }

    public void ChangeRewardSpesialCell(TileHelper tile)
    {
        tile.SetRewardValue(_cellSpesialPositiveEffect[Random.Range(0, _cellSpesialPositiveEffect.Count)]);
        _textWonCell.text = GetRewardValue().ToString();
    }

    private void SetCellListEffects()
    {
        _cellPositiveEffect.Add(5);
        _cellPositiveEffect.Add(10);
        _cellPositiveEffect.Add(15);

        _cellNegativEffect.Add(-10);
        _cellNegativEffect.Add(-15);
        _cellNegativEffect.Add(-25);

        _cellSpesialPositiveEffect.Add(50);
        _cellSpesialPositiveEffect.Add(55);
        _cellSpesialPositiveEffect.Add(65);

        _cellSpesialNegativeEffect.Add(-20);
        _cellSpesialNegativeEffect.Add(-25);
        _cellSpesialNegativeEffect.Add(-30);
    }
}