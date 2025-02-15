using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class FabricCars : MonoBehaviour
{
    [SerializeField] private Cars _cars;
    [SerializeField] private Map _map;

    private Queue<Car> _createdEarlierCars = new Queue<Car>();
    private List<Car> _createdCars = new List<Car>();

    public int NotCreatedCarCount {get; private set;}

    private void Start()
    {
        NotCreatedCarCount = 0;
    }

    public void Create()
    {
        if (_map.CountStartPlace  == 0)
        {
            NotCreatedCarCount += 1;
            Debug.Log(NotCreatedCarCount);

            return;
        }

        Car car = null;

        if (_createdEarlierCars.Count == 0)
            car = Instantiate(_cars.GetRandomCar());
        else
            car = _createdEarlierCars.Dequeue();

        car.transform.rotation = Quaternion.identity;
        car.gameObject.SetActive(true);
        _createdCars.Add(car);

        car.Init(_map.GetStartCarPosition(), _map.GetFinihCarPosition());
    }

    public void PackCars()
    {
        foreach (var car in _createdCars)
        {
            _createdEarlierCars.Enqueue(car);
            car.gameObject.SetActive(false);
        }

        _createdCars = new List<Car>();
    }
}
