﻿using System.Collections.Generic;
using UnityEngine;

public class FerryboatFabric : MonoBehaviour
{
    [SerializeField] private List<Ferryboat> _ferryboats = new List<Ferryboat>();
    [SerializeField] private List<GameObject> _ferryboatGroups = new List<GameObject>();

    private int _currentFerryboat = 0;

    public Ferryboat GetFerryboat()
    {
        for (int i = 0; i < _ferryboats.Count; ++i)
        {
            if (i != _currentFerryboat)
                _ferryboatGroups[i].SetActive(false);
            else
                _ferryboatGroups[i].SetActive(true);
        }

        return _ferryboats[_currentFerryboat];
    }

    public Ferryboat GetNextFerryboat()
    {
        _currentFerryboat += 1;

        return GetFerryboat();
    }

    public bool IsAmout()
    {
        return _currentFerryboat <= 1;
    }
}