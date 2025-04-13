using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleLogic : MonoBehaviour
{
    [SerializeField] private MapLogic _mapLogic;
    [SerializeField] private Game _game;
    [SerializeField] private Soungs _soungs;
    [SerializeField] private ObstacleExplosiveEffector _effector;

    [SerializeField] private PlayerInputController _input;
    [SerializeField] private ObstacleView _obstacleView;

    private List<int[]> _filedTileCoord = new();

    private List<TileHelper> _finishBlockTile = new();
    private List<TileHelper> _finishSpesialBlockTile = new();
    private List<int[]> _damagebaleTile = new();

    private int _valueRanomCreateMoveThan = 5;
    private bool _isBombUsing = false;
    private bool _isFirstRound = true;

    public event Action BombUsed;

    public void Clean()
    {
        _filedTileCoord = new List<int[]>();
        _damagebaleTile = new List<int[]>();
        _finishBlockTile = new List<TileHelper>();
        _finishSpesialBlockTile = new List<TileHelper>();
        _isFirstRound = true;
    }

    public void TryActivateSpesialClicked()
    {
        if (_isBombUsing == false)
            _input.Clicked += OnBombClicked;

        _isBombUsing = true;

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
        if (_isFirstRound == false)
        {
            SetCreatedEarlier();
            return;
        }

        foreach (TileHelper tile in _finishBlockTile)
        {
            _mapLogic.CreatingObstacle(tile.cordX, tile.cordY);
        }

        foreach (TileHelper tile in _finishSpesialBlockTile)
        {
            _mapLogic.CreatingObstacle(tile.cordX, tile.cordY);
        }

        _isFirstRound = false;
    }

    public void SetRandomObstacle()
    {
        if (UnityEngine.Random.Range(0, _valueRanomCreateMoveThan + 1) >= _valueRanomCreateMoveThan)
            return;

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

        _isBombUsing = false;
        _input.Clicked -= OnBombClicked;
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
