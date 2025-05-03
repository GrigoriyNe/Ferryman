using System.Collections.Generic;
using UnityEngine;
using Pool;

namespace CarGroup
{
    public class FabricCars : MonoBehaviour
    {
        [SerializeField] private Cars _cars;
        [SerializeField] private Cars _carsSpesials;
        [SerializeField] private MapFerryboat.MapLogic _map;

        [SerializeField] private Pool<SpawnableObject> _carPool;
        [SerializeField] private Pool<SpawnableObject> _carSpesialPool;

        private List<SpawnableObject> _createdCars = new List<SpawnableObject>();
        private List<SpawnableObject> _createdSpesialCars = new List<SpawnableObject>();

        private Namer _places;

        public void SetPlacesNames(Namer places)
        {
            _places = places;
        }

        public void Create()
        {
            SpawnableObject car = CreatingCar(false);

            car.GetComponent<Car>().Init(
                _map.GetStartCarPosition(), _map.GetFinihCarPosition(), _places);
        }

        public void CreateSpesial()
        {
            SpawnableObject car = CreatingCar(true);

            car.GetComponent<Car>().Init(
                _map.GetStartSpesialCarPosition(), _map.GetSpesialFinihCarPosition(), _places);
        }

        private SpawnableObject CreatingCar(bool isSpesial)
        {
            SpawnableObject car;

            if (isSpesial)
            {
                _carSpesialPool.ChangePrefab(_carsSpesials.GetRandomCar());
                car = _carSpesialPool.GetItem().GetComponent<SpawnableObject>();
                _createdSpesialCars.Add(car);
            }
            else
            {
                _carPool.ChangePrefab(_cars.GetRandomCar());
                car = _carPool.GetItem().GetComponent<SpawnableObject>();
                _createdCars.Add(car);
            }

            car.transform.rotation = Quaternion.identity;
            car.gameObject.SetActive(true);

            return car;
        }

        public void RecoverPositionNotParkCars()
        {
            foreach (SpawnableObject car in _createdCars)
            {
                car.GetComponent<Car>().MoveAway();
            }

            foreach (SpawnableObject car in _createdSpesialCars)
            {
                car.GetComponent<SpesialCar>().MoveAway();
            }
        }

        public void DeactivateCars()
        {
            foreach (SpawnableObject car in _createdCars)
            {
                Deactivating(car);
                _carPool.ReturnItem(car);
            }

            foreach (SpawnableObject car in _createdSpesialCars)
            {
                Deactivating(car);
                _carSpesialPool.ReturnItem(car);
            }

            _createdCars = new List<SpawnableObject>();
            _createdSpesialCars = new List<SpawnableObject>();
        }

        private void Deactivating(SpawnableObject car)
        {
            car.transform.position = Vector3.zero;
            car.gameObject.SetActive(false);
        }
    }
}