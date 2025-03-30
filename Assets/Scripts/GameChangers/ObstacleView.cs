using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class ObstacleView : MonoBehaviour
{
    [SerializeField] private ObstaclesSprites _spritesClosePark;
    [SerializeField] private ObstaclesSprites _spritesOpenPark;
    [SerializeField] private ObstaclesSprites _spritesEmpty;
    [SerializeField] private ObstaclesSprites _spritesOpenParkSpesial;
    [SerializeField] private ObstaclesSprites _spritesCloseParkSpesial;

    [SerializeField] private GameObject _obstaclePrefab;
    [SerializeField] private GameObject _obstacleSpesialPrefab;
    [SerializeField] private GameObject _obstacleSpesialDamagemaledPrefab;

    private List<GameObject> _filedObstacle = new();

    private void OnDisable()
    {
        for (int i = 0; i < _filedObstacle.Count; i++)
        {
            if (_filedObstacle[i].IsDestroyed() == false)
                _filedObstacle[i].gameObject.SetActive(false);
        }

        _filedObstacle = new();
    }

    public Sprite GetSpriteClose(Transform target)
    {
        SetObstale(target);
        return _spritesClosePark.GetSprite();
    }

    public Sprite GetSpriteOpen(Transform target)
    {
        RemoveObstacle(target);
        return _spritesOpenPark.GetSprite();
    }

    public Sprite GetSpriteOpenSpesial(Transform target)
    {
        RemoveObstacle(target);
        return _spritesOpenParkSpesial.GetSprite();
    }

    public Sprite GetSpriteCloseSpesial(Transform target)
    {
        SetObstaleSpesial(target);
        return _spritesCloseParkSpesial.GetSprite();
    }

    public void GetDamgaeBox(Transform target)
    {
        RemoveObstacle(target);

        GameObject obstacle = Instantiate(_obstacleSpesialDamagemaledPrefab, target.position, Quaternion.identity);

        _filedObstacle.Add(obstacle);
        obstacle.gameObject.SetActive(true);
    }

    public Sprite GetSpriteEmpty()
    {
        return _spritesEmpty.GetSprite();
    }

    private void SetObstale(Transform target)
    {
        GameObject obstacle = Instantiate(_obstaclePrefab, target.position, Quaternion.identity);

        _filedObstacle.Add(obstacle);
        obstacle.gameObject.SetActive(true);
    }

    private void SetObstaleSpesial(Transform target)
    {
        GameObject obstacle = Instantiate(_obstacleSpesialPrefab, target.position, Quaternion.identity);

        _filedObstacle.Add(obstacle);
        obstacle.gameObject.SetActive(true);
    }

    private void RemoveObstacle(Transform target)
    {
        for (int i = 0; i < _filedObstacle.Count; i++)
        {
            if (_filedObstacle[i].transform.position == target.position)
            {
                _filedObstacle[i].gameObject.SetActive(false);
                _filedObstacle.Remove(_filedObstacle[i]);
            }
        }
    }
}
