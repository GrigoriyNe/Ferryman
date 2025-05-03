using System.Collections.Generic;
using UnityEngine;
using FerryboatGroup;

namespace FerryboatGroup
{
    public class FerryboatFabric : MonoBehaviour
    {
        [SerializeField] private List<Ferryboat> _ferryboats = new List<Ferryboat>();
        [SerializeField] private List<GameObject> _ferryboatGroups = new List<GameObject>();

        public int FerryboatsCount => _ferryboats.Count;

        public Ferryboat GetFerryboat(int value)
        {
            for (int i = 0; i < _ferryboats.Count; ++i)
            {
                _ferryboatGroups[i].SetActive(false);
            }

            _ferryboatGroups[value].SetActive(true);

            return _ferryboats[value];
        }
    }
}