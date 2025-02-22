using UnityEngine;

public class ObstacleLogic : MonoBehaviour
{
    [SerializeField] private ObstacleView _view;
    [SerializeField] private Map _map;

    public void CreateObstacle()
    {

        _map.CreateObstacle();

        Debug.Log("323");

        if (Random.Range(0, 20) % 5 == 0)
            _map.CreateObstacle();
    }
}
