﻿using System.Collections.Generic;
using UnityEngine;

namespace CarGroup
{
    public class ParkPlaceSecondFerry : Namer
    {
        [SerializeField] private MapFerryboat.Map _map;

        public override void FillDictionary()
        {
            _placesVertical.Add(0, "A");
            _placesVertical.Add(1, "B");
            _placesVertical.Add(2, "C");
            _placesVertical.Add(3, "D");
            _placesVertical.Add(4, "E");
            _placesVertical.Add(5, "f");

            int height = _map.GetHeight();

            for (int i = height - 6; i < height;)
            {
                for (int j = 1; j <= (height - 7);)
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

            if (_placesVertical.ContainsKey(vertical) && vertical == 4)
            {
                if (_placesHorizontal.ContainsKey(horizontal) && horizontal == _map.GetHeight() - 2)
                {
                    return "g";
                }

                if (_placesHorizontal.ContainsKey(horizontal) && horizontal == _map.GetHeight() - 4)
                {
                    return "h";
                }

                if (_placesHorizontal.ContainsKey(horizontal) && horizontal == _map.GetHeight() - 6)
                {
                    return "i";
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