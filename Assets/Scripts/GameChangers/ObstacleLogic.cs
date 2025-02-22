using UnityEngine;

public class ObstacleLogic : MonoBehaviour
{
    [SerializeField] private ObstacleView _view;
    [SerializeField] private Map _map;

    public void CreateObstacle()
    {
        if (Random.Range(0, 20) % 5 == 0)
            _map.CreateObstacle();
    }
}
