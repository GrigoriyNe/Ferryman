using System.Collections.Generic;
using UnityEngine;
using Pool;
using TileGroup;

namespace CarGroup
{
    public class FabricCars : MonoBehaviour
    {
        [SerializeField] private MapFerryboat.MapLogic _map;
        [SerializeField] private Car _prefabCar;
        [SerializeField] private Car _prefabSpesialCar;

        [SerializeField] private List<CarPropery> _carsPropertys;
        [SerializeField] private List<CarPropery> _spesialCarsPropertys;

        private Pool<Car> _carPool;
        private Pool<Car> _carSpesialPool;

        private List<Car> _createdCars = new List<Car>();
        private List<Car> _createdSpesialCars = new List<Car>();

        private List<Tile> _regularPlaces = new List<Tile>();
        private List<Tile> _spesialPlaces = new List<Tile>();

        private Namer _places;

        private void Awake()
        {
            _carPool = new Pool<Car>(_prefabCar);
            _carSpesialPool = new Pool<Car>(_prefabSpesialCar);
        }

        public void SetPlacesNames(Namer places)
        {
            _places = places;
        }

        public void SetParkPlace(List<Tile> tiles)
        {
            foreach (var tile in tiles)
            {
                if (tile.TileType == Tile.Type.Regular)
                {
                    _regularPlaces.Add(tile);
                }
                else
                {
                    _spesialPlaces.Add(tile);
                }
            }
        }

        public void Create()
        {
            Car car = CreatingCar(false);
            Color color = _carsPropertys[Random.Range(
                0,
                _spesialCarsPropertys.Count)].GetColor;

            Tile tile = _regularPlaces[Random.Range(
                0,
                _regularPlaces.Count)];

            car.GetComponent<Car>().Init(
               color,
               _map.GetStartCarPosition(),
               tile,
               _places);

            _regularPlaces.Remove(tile);
        }

        public void CreateSpesial()
        {
            Car car = CreatingCar(true);
            Color color = _spesialCarsPropertys[Random.Range(
                0,
                _spesialCarsPropertys.Count)].GetColor;

            Tile tile = _spesialPlaces[Random.Range(
                0,
                _spesialPlaces.Count)];

            car.GetComponent<Car>().Init(
               color,
               _map.GetStartCarPosition(),
               tile,
               _places);

            _spesialPlaces.Remove(tile);
        }

        private Car CreatingCar(bool isSpesial)
        {
            Car car;

            if (isSpesial)
            {
                car = _carSpesialPool.GetItem().GetComponent<Car>();
                _createdSpesialCars.Add(car);
            }
            else
            {
                car = _carPool.GetItem().GetComponent<Car>();
                _createdCars.Add(car);
            }

            car.transform.rotation = Quaternion.identity;
            car.gameObject.SetActive(true);

            return car;
        }

        public void RecoverPositionNotParkCars()
        {
            foreach (Car car in _createdCars)
            {
                car.GetComponent<Car>().MoveAway();
            }

            foreach (Car car in _createdSpesialCars)
            {
                car.GetComponent<SpesialCar>().MoveAway();
            }
        }

        public void DeactivateCars()
        {
            foreach (Car car in _createdCars)
            {
                Deactivating(car);
                _carPool.ReturnItem(car);
            }

            foreach (Car car in _createdSpesialCars)
            {
                Deactivating(car);
                _carSpesialPool.ReturnItem(car);
            }

            _createdCars = new List<Car>();
            _createdSpesialCars = new List<Car>();
        }

        private void Deactivating(Car car)
        {
            car.transform.position = Vector3.zero;
            car.gameObject.SetActive(false);
        }
    }
}