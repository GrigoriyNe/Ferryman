using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleLogic : MonoBehaviour
{
    private const int Offset = 1;

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

    private int _maxValueSpawnOpstacles = 3;
    private bool _isBombUsing = false;
    private bool _isFirstRound = true;

    public event Action BombUsed;

    public event Action BombUse;

    public event Action BombCanceled;

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
        if (_isBombUsing)
        {
            _input.Clicked -= OnBombClicked;
            BombCanceled?.Invoke();

            for (int i = 0; i < _filedTileCoord.Count; i++)
            {
                _mapLogic.GetTile(
                    _filedTileCoord[i].GetValue(0).ConvertTo<int>(),
                    _filedTileCoord[i].GetValue(1)
                    .ConvertTo<int>()).DectivateLigther();
            }

            _isBombUsing = false;

            return;
        }

        BombUse?.Invoke();
        _isBombUsing = true;
        _input.Clicked += OnBombClicked;

        for (int i = 0; i < _filedTileCoord.Count; i++)
        {
            _mapLogic.GetTile(
                _filedTileCoord[i].GetValue(0).ConvertTo<int>(),
                _filedTileCoord[i].GetValue(1)
                .ConvertTo<int>()).ActivateLigther();
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
            _mapLogic.CreatingObstacle(tile.CordX, tile.CordY);
        }

        foreach (TileHelper tile in _finishSpesialBlockTile)
        {
            _mapLogic.CreatingObstacle(tile.CordX, tile.CordY);
        }

        _isFirstRound = false;
    }

    public void SetRandomObstacle()
    {
        int countCreateOstacles = UnityEngine.Random.Range(
            0, _maxValueSpawnOpstacles + Offset);

        for (int i = 0; i < countCreateOstacles; i++)
        {
            _mapLogic.CreateRandomObstacle();
        }
    }

    public void RememberObstacle(TileHelper filedTile)
    {
        _filedTileCoord.Add(new int[] { filedTile.CordX, filedTile.CordY });
    }

    private void OnBombClicked(int x, int y)
    {
        if (_mapLogic.CheckObstacle(x, y) == false)
            return;

        if (y < _mapLogic.RoadOffVerticalValue + 2)
            return;

        _input.Clicked -= OnBombClicked;
        _isBombUsing = false;

        TileHelper tile = _mapLogic.GetTile(x, y);
        bool isDamagebaleTakenErlear = false;
        _soungs.PlayBombSoung();
        _effector.ExplosiveEffects(tile.transform.position);

        for (int i = 0; i < _filedTileCoord.Count; i++)
        {
            _mapLogic.GetTile(
                _filedTileCoord[i].GetValue(0).ConvertTo<int>(), _filedTileCoord[i]
                .GetValue(1).ConvertTo<int>()).DectivateLigther();
        }

        if (_finishSpesialBlockTile.Contains(tile))
        {
            for (int i = 0; i < _damagebaleTile.Count; i++)
            {
                if (_damagebaleTile[i].GetValue(0).ConvertTo<int>() == tile.CordX)
                {
                    if (_damagebaleTile[i].GetValue(1).ConvertTo<int>() == tile.CordY)
                    {
                        _damagebaleTile.Remove(_damagebaleTile[i]);
                        isDamagebaleTakenErlear = true;
                        ExplosiveBomb(tile);

                        return;
                    }
                }
            }

            if (isDamagebaleTakenErlear == false)
            {
                _damagebaleTile.Add(new int[] { tile.CordX, tile.CordY });
                _input.Clicked -= OnBombClicked;
                BombUsed?.Invoke();
                _obstacleView.GetDamgaeBox(tile.transform);

                return;
            }
        }

        ExplosiveBomb(tile);
    }

    private void ExplosiveBomb(TileHelper tile)
    {
        BombUsed?.Invoke();

        for (int i = 0; i < _filedTileCoord.Count; i++)
        {
            if (_filedTileCoord[i].GetValue(0).ConvertTo<int>() == tile.CordX)
            {
                if (_filedTileCoord[i].GetValue(1).ConvertTo<int>() == tile.CordY)
                {
                    _filedTileCoord.Remove(_filedTileCoord[i]);
                }
            }
        }

        _mapLogic.DeleteObstacle(tile.CordX, tile.CordY);
    }

    private void SetCreatedEarlier()
    {
        for (int i = 0; i < _filedTileCoord.Count;)
        {
            _mapLogic.CreatingObstacle(
                _filedTileCoord[i].GetValue(0).ConvertTo<int>(),
                _filedTileCoord[i].GetValue(1).ConvertTo<int>());
            i += 1;
        }

        SetCreatedErlealerHalfDamageObstacles();
    }

    private void SetCreatedErlealerHalfDamageObstacles()
    {
        if (_damagebaleTile.Count > 0)
        {
            for (int i = 0; i < _damagebaleTile.Count;)
            {
                TileHelper halfDamageBox = _mapLogic.GetTile(
                    _damagebaleTile[i].GetValue(0).ConvertTo<int>(),
                    _damagebaleTile[i].GetValue(1).ConvertTo<int>());

                _obstacleView.GetDamgaeBox(halfDamageBox.transform);
                i++;
            }
        }
    }
}
