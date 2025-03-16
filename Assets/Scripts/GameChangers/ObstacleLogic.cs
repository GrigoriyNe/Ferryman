using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleLogic : MonoBehaviour
{
    [SerializeField] private MapLogic _mapLogic;
    [SerializeField] private Game _game;

    [SerializeField] private PlayerInputController _input;

    private List<int[]> _filedTileCoord = new();

    private List<TileHelper> _startBlockTile = new();
    private List<TileHelper> _startSpesialBlockTile = new();

    private bool _isSpesialSelected = false;
    private int _maxRangeForRandomCreatigVarible = 20;

    public void Clean()
    {
        _filedTileCoord = new List<int[]>();
        _startBlockTile = new List<TileHelper>();
        _startSpesialBlockTile = new List<TileHelper>();
    }

    public void ActivateClicked()
    {
        _input.Clicked += OnClicked;
        _isSpesialSelected = false;
    }

    public void ActivateSpesialClicked()
    {
        _input.Clicked += OnClicked;
        _isSpesialSelected = true;
    }

    public void SetBlockedStarPlace(TileHelper tile)
    {
        if (_startBlockTile.Contains(tile))
            return;

        _startBlockTile.Add(tile);
    }

    public void SetSpesialBlockedStarPlace(TileHelper tile)
    {
        if (_startSpesialBlockTile.Contains(tile))
            return;

        _startSpesialBlockTile.Add(tile);
    }

    public void SmalerVaribleCreating()
    {
        if (_maxRangeForRandomCreatigVarible + 1 < 25)
            _maxRangeForRandomCreatigVarible += 1;
    }

    public void CreateObstacle()
    {
        if (_filedTileCoord.Count > 0)
            SetCreatedEarlier();

        if (_filedTileCoord.Count < 1)
        {
            //      TileHelper boofer = _mapLogic.GetTile(_mapLogic.RoadOffVerticalValue, _mapLogic.RoadOffVerticalValue);
            //      _mapLogic.CreatingObstacle(boofer);

            foreach (TileHelper tile in _startBlockTile)
            {
                _mapLogic.CreatingObstacle(tile.cordX, tile.cordY);
            }

            foreach (TileHelper tile in _startSpesialBlockTile)
            {
                _mapLogic.CreatingObstacle(tile.cordX, tile.cordY);
            }
        }
    }

    public void SetRandomObstacle()
    {
        if (UnityEngine.Random.Range(0, _maxRangeForRandomCreatigVarible) % 3 == 0)
            _mapLogic.CreateRandomObstacle();
    }

    public void RememberObstacle(TileHelper filedTile)
    {
        _filedTileCoord.Add(new int[] { filedTile.cordX, filedTile.cordY });
    }

    private void OnClicked(int x, int y)
    {
        if (_mapLogic.CheckObstacle(x, y) == false)
            return;

        if (y < _mapLogic.RoadOffVerticalValue + 2)
            return;

        TileHelper tile = _mapLogic.GetTile(x, y);

        if (_isSpesialSelected)
        {
            if (_startSpesialBlockTile.Contains(tile) == false)
            {
                return;
            }
        }
        else
        {
            if (_startSpesialBlockTile.Contains(tile))
            {
                return;
            }
        }

        _input.Clicked -= OnClicked;
        _filedTileCoord.Remove(new int[] { tile.cordX, tile.cordY });

        _mapLogic.DeleteObstacle(tile.cordX, tile.cordY);
        _game.CreateNewCar();
        _isSpesialSelected = false;
    }

    private void SetCreatedEarlier()
    {
        for (int i = 0; i < _filedTileCoord.Count;)
        {
            _mapLogic.CreatingObstacle(_filedTileCoord[i].GetValue(0).ConvertTo<int>(), _filedTileCoord[i].GetValue(1).ConvertTo<int>());
            i += 1;
        }
    }
}
