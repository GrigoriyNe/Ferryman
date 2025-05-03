using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Obstacle
{
    public class ObstacleView : MonoBehaviour
    {
        [SerializeField] private GameObject _obstaclePrefab;
        [SerializeField] private GameObject _obstacleSpesialPrefab;
        [SerializeField] private GameObject _obstacleSpesialDamagemaledPrefab;

        private List<GameObject> _filedObstacles = new();

        private void OnDisable()
        {
            for (int i = 0; i < _filedObstacles.Count; i++)
            {
                if (_filedObstacles[i].IsDestroyed() == false)
                    _filedObstacles[i].gameObject.SetActive(false);
            }

            _filedObstacles = new();
        }

        public void SetObstacle(Transform target)
        {
            SetObstale(target);
        }

        public void SetObstacleSpesial(Transform target)
        {
            SetObstaleSpesial(target);
        }

        public void GetDamgaeBox(Transform target)
        {
            RemoveObstacle(target);

            GameObject obstacle = Instantiate(_obstacleSpesialDamagemaledPrefab, target.position, Quaternion.identity);

            _filedObstacles.Add(obstacle);
            obstacle.gameObject.SetActive(true);
        }

        public void RemoveObstacle(Transform target)
        {
            for (int i = 0; i < _filedObstacles.Count; i++)
            {
                if (_filedObstacles[i].transform.position == target.position)
                {
                    _filedObstacles[i].gameObject.SetActive(false);
                    _filedObstacles.Remove(_filedObstacles[i]);
                }
            }
        }

        private void SetObstale(Transform target)
        {
            GameObject obstacle = Instantiate(_obstaclePrefab, target.position, Quaternion.identity);

            _filedObstacles.Add(obstacle);
            obstacle.gameObject.SetActive(true);
        }

        private void SetObstaleSpesial(Transform target)
        {
            GameObject obstacle = Instantiate(_obstacleSpesialPrefab, target.position, Quaternion.identity);

            _filedObstacles.Add(obstacle);
            obstacle.gameObject.SetActive(true);
        }
    }
}