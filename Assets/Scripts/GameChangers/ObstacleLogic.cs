using System;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleLogic : MonoBehaviour
{
    [SerializeField] private MapLogic _mapLogic;
    [SerializeField] private Game _game;

    [SerializeField] private PlayerInputController _input;

    private List<int> _filedTileCoordX = new();
    private List<int> _filedTileCoordY = new();

    private List<TileHelper> _startBlockTile = new();
    private List<TileHelper> _startSpesialBlockTile = new();

    private bool _isSpesialSelected = false;
    private int _maxRangeForRandomCreatigVarible = 20;

    public void Clean()
    {
        _filedTileCoordX = new List<int>();
        _filedTileCoordY = new List<int>();
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
        _startBlockTile.Add(tile);
    }

    public void SetSpesialBlockedStarPlace(TileHelper tile)
    {
        _startSpesialBlockTile.Add(tile);
    }

    public void SmalerVaribleCreating()
    {
        if (_maxRangeForRandomCreatigVarible + 1 < 25)
            _maxRangeForRandomCreatigVarible += 1;
    }

    public void CreateObstacle()
    {
        if (_filedTileCoordX.Count < 1)
        {
            TileHelper boofer = _mapLogic.GetTile(_mapLogic.RoadOffVerticalValue, _mapLogic.RoadOffVerticalValue);
            _mapLogic.CreatingObstacle(boofer);

            foreach (TileHelper tile in _startBlockTile)
            {
                _mapLogic.CreatingObstacle(tile);
            }

            foreach (TileHelper tile in _startSpesialBlockTile)
            {
                _mapLogic.CreatingObstacle(tile);
            }
        }

        if (_filedTileCoordX.Count > 0)
            SetCreatedEarlier();

        if (UnityEngine.Random.Range(0, _maxRangeForRandomCreatigVarible) % 5 == 0)
            _mapLogic.CreateObstacle();
    }

    public void RememberObstacle(List<TileHelper> filedTile)
    {
        foreach (TileHelper tile in filedTile)
        {
            _filedTileCoordX.Add(tile.cordX);
            _filedTileCoordY.Add(tile.cordY);
        }
    }

    public void RemoveObstacle(List<TileHelper> filedTile)
    {
        _filedTileCoordX = new List<int>();
        _filedTileCoordY = new List<int>();
        RememberObstacle(filedTile);
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

        _filedTileCoordX.Remove(tile.cordX);
        _filedTileCoordY.Remove(tile.cordY);

        _mapLogic.DeleteObstacle(tile.cordX, tile.cordY);
        _game.CreateNewCar();
        _isSpesialSelected = false;
    }

    private void SetCreatedEarlier()
    {
        for (int i = 0; i < _filedTileCoordX.Count;)
        {
            _mapLogic.CreatingObstacle(_mapLogic.GetTile(_filedTileCoordX[i], _filedTileCoordY[i]));
            i += 1;
        }
    }
}
