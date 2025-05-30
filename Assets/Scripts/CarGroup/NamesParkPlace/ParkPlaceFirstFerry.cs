﻿using UnityEngine;

namespace CarGroup
{
    public class ParkPlaceFirstFerry : Namer
    {
        [SerializeField] private MapFerryboat.Map _map;

        public override void FillDictionary()
        {
            _placesVertical.Add(0, "A");
            _placesVertical.Add(1, "B");
            _placesVertical.Add(2, "C");
            _placesVertical.Add(3, "D");

            int height = _map.GetHeight();

            for (int i = height - 6; i < height;)
            {
                for (int j = 1; j <= (height - 5);)
                {
                    _placesHorizontal.Add(i, j);
                    i++;
                    j++;
                }
            }
        }

        public override string GetTextPlace(int vertical, int horizontal)
        {
            string resultVertical = string.Empty;
            string resultHorizontal = string.Empty;

            if (vertical == 4)
            {
                if (horizontal == _map.GetHeight() - 3)
                {
                    return "e";
                }
                else if (horizontal == _map.GetHeight() - 4)
                {
                    return "f";
                }
                else if (horizontal == _map.GetHeight() - 5)
                {
                    return "g";
                }
            }

            if (_placesVertical.ContainsKey(vertical))
                resultVertical = _placesVertical[vertical].ToString();

            if (_placesHorizontal.ContainsKey(horizontal))
                resultHorizontal = _placesHorizontal[horizontal].ToString();

            string result = resultVertical + resultHorizontal;

            return result;
        }
    }
}