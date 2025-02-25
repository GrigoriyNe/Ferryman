using System;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleLogic : MonoBehaviour
{
    [SerializeField] private Map _map;
    [SerializeField] private MapLogic _mapLogic;
    [SerializeField] private Game _game;

    [SerializeField] private PlayerInputController _input;

    private List<int> _filedTileCoordX = new();
    private List<int> _filedTileCoordY = new();

    private List<TileHelper> _startBlockTile = new();
    private List<TileHelper> _startSpesialBlockTile = new();

    public void Activate()
    {
        _input.Clicked += OnClicked;
    }

    public void SetBlockedStarPlace(TileHelper tile)
    {
        _startBlockTile.Add(tile);

    }

    public void SetSpesialBlockedStarPlace(TileHelper tile)
    {
        _startSpesialBlockTile.Add(tile);
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

        if (UnityEngine.Random.Range(0, 20) % 5 == 0)
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
        _filedTileCoordX = new();
        _filedTileCoordY = new();
        RememberObstacle(filedTile);
    }

    private void OnClicked(TileHelper tile)
    {
        if (_mapLogic.CheckObstacle(tile.cordX, tile.cordY) == false)
            return;

        if (tile.cordY < _mapLogic.RoadOffVerticalValue + 2)
            return;

        _filedTileCoordX.Remove(tile.cordX);
        _filedTileCoordY.Remove(tile.cordY);

        _map.RemoveObstacle(tile);
        _game.CreateNewCar();
        _input.Clicked -= OnClicked;
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
