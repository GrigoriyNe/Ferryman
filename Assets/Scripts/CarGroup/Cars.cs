using System.Collections.Generic;
using UnityEngine;
using Pool;

namespace CarGroup
{
    public class Cars : MonoBehaviour
    {
        [SerializeField] private List<SpawnableObject> _cars;

        public SpawnableObject GetRandomCar()
        {
            return _cars[Random.Range(0, _cars.Count)];
        }
    }
}