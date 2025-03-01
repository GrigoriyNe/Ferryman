using System;
using System.Collections.Generic;
using UnityEngine;

public class FabricCars : MonoBehaviour
{
    [SerializeField] private Cars _cars;
    [SerializeField] private SpesialCars _carsSpesials;
    [SerializeField] private MapLogic _map;

    private Queue<Car> _createdEarlierCars = new Queue<Car>();
    private List<Car> _createdCars = new List<Car>();
    private List<SpesialCar> _createdSpesialCars = new List<SpesialCar>();

    private Namer _places;

    public int NotCreatedCarCount {get; private set;}
    public int NotCreatedSpesialCarCount { get; private set; }

    private void Start()
    {
        NotCreatedCarCount = 0;
        NotCreatedSpesialCarCount = 0;
    }

    public void SetPlacesNames(Namer places)
    {
        _places = places;
    }

    public void Create()
    {
        if (_map.CountFinishPlace  == 0)
        {
            NotCreatedCarCount += 1;

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

        car.Init(_map.GetStartCarPosition(), _map.GetFinihCarPosition(), _places);
    }

    public void CreateSpesial()
    {
        if (_map.CountStartSpesialPlace == 0)
        {
            NotCreatedSpesialCarCount += 1;

            return;
        }

        SpesialCar car = null;

        car = Instantiate(_carsSpesials.GetRandomCar());
        car.transform.rotation = Quaternion.identity;
        car.gameObject.SetActive(true);

        _createdSpesialCars.Add(car);

        car.Init(_map.GetStartSpesialCarPosition(), _map.GetSpesialFinihCarPosition(), _places);
    }

    public void DeactivateCars()
    {
        foreach (Car car in _createdCars)
        {
            car.gameObject.SetActive(false);
            _createdEarlierCars.Enqueue(car);
        }

        foreach (SpesialCar car in _createdSpesialCars)
        {
            car.gameObject.SetActive(false);
        }

        _createdCars = new List<Car>();
        _createdSpesialCars = new List<SpesialCar>();
    }
}
