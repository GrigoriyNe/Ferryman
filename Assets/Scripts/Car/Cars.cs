﻿using System.Collections.Generic;
using UnityEngine;

public class Cars : MonoBehaviour
{
    [SerializeField] private List<Car> _cars;

    public Car GetRandomCar()
    {
        return _cars[Random.Range(0, _cars.Count)];
    }
}
