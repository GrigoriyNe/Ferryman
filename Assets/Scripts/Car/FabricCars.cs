using System.Collections.Generic;
using UnityEngine;

public class FabricCars : MonoBehaviour
{
    [SerializeField] private Cars _cars;
    [SerializeField] private SpesialCars _carsSpesials;
    [SerializeField] private MapLogic _map;

    [SerializeField] private CarPool _carPool;
    [SerializeField] private CarSpesialPool _carSpesialPool;

    private List<Car> _createdCars = new List<Car>();
    private List<SpesialCar> _createdSpesialCars = new List<SpesialCar>();

    private Namer _places;

    public void SetPlacesNames(Namer places)
    {
        _places = places;
    }

    public void Create()
    {
        Car car = null;
        car = _carPool.GetItem().GetComponent<Car>();
        _carPool.ChangePrefab(_cars.GetRandomCar());

        car.transform.rotation = Quaternion.identity;
        car.gameObject.SetActive(true);
        _createdCars.Add(car);

        car.Init(_map.GetStartCarPosition(), _map.GetFinihCarPosition(), _places);
    }

    public void CreateSpesial()
    {
        SpesialCar car = null;
        car = _carSpesialPool.GetItem().GetComponent<SpesialCar>();
        _carSpesialPool.ChangePrefab(_carsSpesials.GetRandomCar());

        car.transform.rotation = Quaternion.identity;
        car.gameObject.SetActive(true);

        _createdSpesialCars.Add(car);

        car.Init(_map.GetStartSpesialCarPosition(), _map.GetSpesialFinihCarPosition(), _places);
    }

    public void RecoverPositionNotParkCars()
    {
        foreach (Car car in _createdCars)
        {
            car.MoveAway();
        }

        foreach (SpesialCar car in _createdSpesialCars)
        {
            car.MoveAway();
        }
    }

    public void DeactivateCars()
    {
        foreach (Car car in _createdCars)
        {
            car.transform.position = Vector3.zero;
            car.gameObject.SetActive(false);
            _carPool.ReturnItem(car);
        }

        foreach (SpesialCar car in _createdSpesialCars)
        {
            car.transform.position = Vector3.zero;
            car.gameObject.SetActive(false);
            _carSpesialPool.ReturnItem(car);
        }

        _createdCars = new List<Car>();
        _createdSpesialCars = new List<SpesialCar>();
    }
}