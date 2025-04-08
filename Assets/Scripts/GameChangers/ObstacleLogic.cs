using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleLogic : MonoBehaviour
{
    [SerializeField] private MapLogic _mapLogic;
    [SerializeField] private Game _game;
    [SerializeField] private Soungs _soungs;
    [SerializeField] private int _valueDividerRanomCreate = 3;
    [SerializeField] private ObstacleExplosiveEffector _effector;

    [SerializeField] private PlayerInputController _input;
    [SerializeField] private ObstacleView _obstacleView;

    private List<int[]> _filedTileCoord = new();

    private List<TileHelper> _finishBlockTile = new();
    private List<TileHelper> _finishSpesialBlockTile = new();
    private List<int[]> _damagebaleTile = new();

    private int _maxRangeForRandomCreatigVarible = 20;

    public event Action BombUsed;

    public void Clean()
    {
        _filedTileCoord = new List<int[]>();
        _damagebaleTile = new List<int[]>();
        _finishBlockTile = new List<TileHelper>();
        _finishSpesialBlockTile = new List<TileHelper>();
    }

    public void TryActivateSpesialClicked()
    {
        _input.Clicked += OnBombClicked;

        for (int i = 0; i < _filedTileCoord.Count; i++)
        {
            _mapLogic.GetTile(_filedTileCoord[i].GetValue(0).ConvertTo<int>(), _filedTileCoord[i].GetValue(1).ConvertTo<int>()).ActivateLigther();
        }
    }

    public void SetBlockedFinishPlace(TileHelper tile)
    {
        if (_finishBlockTile.Contains(tile))
            return;

        _finishBlockTile.Add(tile);
    }

    public void SetSpesialFinishPlace(TileHelper tile)
    {
        if (_finishSpesialBlockTile.Contains(tile))
            return;

        _finishSpesialBlockTile.Add(tile);
    }

    public void CreateObstacle()
    {
        if (_filedTileCoord.Count > 0)
            SetCreatedEarlier();

        if (_filedTileCoord.Count < 1)
        {
            _mapLogic.CreatingObstacle(_mapLogic.RoadOffVerticalValue, _mapLogic.RoadOffVerticalValue);

            foreach (TileHelper tile in _finishBlockTile)
            {
                _mapLogic.CreatingObstacle(tile.cordX, tile.cordY);
            }

            foreach (TileHelper tile in _finishSpesialBlockTile)
            {
                _mapLogic.CreatingObstacle(tile.cordX, tile.cordY);
            }
        }
    }

    public void SetRandomObstacle()
    {
        if (UnityEngine.Random.Range(0, _maxRangeForRandomCreatigVarible) % _valueDividerRanomCreate == 0)
            _mapLogic.CreateRandomObstacle();
    }

    public void RememberObstacle(TileHelper filedTile)
    {
        _filedTileCoord.Add(new int[] { filedTile.cordX, filedTile.cordY });
    }

    private void OnBombClicked(int x, int y)
    {
        if (_mapLogic.CheckObstacle(x, y) == false)
            return;

        if (y < _mapLogic.RoadOffVerticalValue + 2)
            return;

        TileHelper tile = _mapLogic.GetTile(x, y);
        bool isDamagebaleTakenErlear = false;
        _soungs.PlayBombSoung();
        _effector.ExplosiveEffects(tile.transform.position);

        for (int i = 0; i < _filedTileCoord.Count; i++)
        {
            _mapLogic.GetTile(_filedTileCoord[i].GetValue(0).ConvertTo<int>(), _filedTileCoord[i].GetValue(1).ConvertTo<int>()).DectivateLigther();
        }

        if (_finishSpesialBlockTile.Contains(tile))
        {
            for (int i = 0; i < _damagebaleTile.Count; i++)
            {
                if (_damagebaleTile[i].GetValue(0).ConvertTo<int>() == tile.cordX)
                {
                    if (_damagebaleTile[i].GetValue(1).ConvertTo<int>() == tile.cordY)
                    {
                        _damagebaleTile.Remove(_damagebaleTile[i]);
                        isDamagebaleTakenErlear = true;
                    }
                }
            }

            if (isDamagebaleTakenErlear == false)
            {
                _damagebaleTile.Add(new int[] { tile.cordX, tile.cordY });
                _input.Clicked -= OnBombClicked;
                BombUsed?.Invoke();
                _obstacleView.GetDamgaeBox(tile.transform);

                return;
            }
        }

        _input.Clicked -= OnBombClicked;
        BombUsed?.Invoke();

        for (int i = 0; i < _filedTileCoord.Count; i++)
        {
            if (_filedTileCoord[i].GetValue(0).ConvertTo<int>() == tile.cordX)
                if (_filedTileCoord[i].GetValue(1).ConvertTo<int>() == tile.cordY)
                    _filedTileCoord.Remove(_filedTileCoord[i]);
        }

        _mapLogic.DeleteObstacle(tile.cordX, tile.cordY);
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
