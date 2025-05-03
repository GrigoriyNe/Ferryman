using System.Collections.Generic;
using UnityEngine;

namespace CarGroup
{
    public class SpesialCars : MonoBehaviour
    {
        [SerializeField] private List<SpesialCar> _cars;

        public SpesialCar GetRandomCar()
        {
            return _cars[Random.Range(0, _cars.Count)];
        }
    }
}