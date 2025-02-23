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

    public int FiledTileCout => _filedTileCoordX.Count;

    public void Activate()
    {
        _input.Clicked += OnClicked;
    }

    public void CreateObstacle()
    {
        if (_filedTileCoordX.Count > 0)
            SetCreatedEarlier();

        _map.CreateObstacle();

        if (UnityEngine.Random.Range(0, 20) % 5 == 0)
            _map.CreateObstacle();
    }

    public void RememberObstacle(List<TileHelper> filedTile)
    {
        foreach (TileHelper tileEarly in filedTile)
        {
            _filedTileCoordX.Add(tileEarly.cord_x);
            _filedTileCoordY.Add(tileEarly.cord_y);
        }
    }

    private void OnClicked(TileHelper tile)
    {
        if (_mapLogic.CheckObstacle(tile.cord_x, tile.cord_y) == false)
            return;

        if (tile.cord_y < _mapLogic.RoadOffVerticalValue + 2)
            return;

        _filedTileCoordX.Remove(tile.cord_x);
        _filedTileCoordY.Add(tile.cord_y);

        _map.RemoveObstacle(tile);
        _game.CreateNewCar();
        _input.Clicked -= OnClicked;
    }

    private void SetCreatedEarlier()
    {
        for (int i = 0; i < _filedTileCoordX.Count; i++)
        {
            _mapLogic.CreatingObstacle(_mapLogic.GetTile(_filedTileCoordX[i], _filedTileCoordY[i]));
        }
    }
}
