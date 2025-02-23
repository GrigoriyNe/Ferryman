using System.Collections.Generic;
using UnityEngine;

public class ShipAdder : MonoBehaviour
{
   [SerializeField] private List<Ferryboat> _ferryboats = new List<Ferryboat>();
    [SerializeField] private List<GameObject> _ferryboatGroups= new List<GameObject>();

    public Ferryboat GetFerryboat(int count)
    {
        foreach (GameObject item in _ferryboatGroups)
        {
            item.SetActive(false);
        }

        _ferryboatGroups[count].SetActive(true);

        return _ferryboats[count];
    }
}