using System.Collections.Generic;
using UnityEngine;

public class NamesOfParkingPlaces : MonoBehaviour
{
    [SerializeField] Map _map;

    private Dictionary<int, string> _placesVertical;
    private Dictionary<int, int> _placesHorizontal;

    private void OnEnable()
    {
        _placesVertical = new Dictionary<int, string>();
        _placesHorizontal = new Dictionary<int, int>();
        FillDictionary();
    }

    private void FillDictionary()
    {
        _placesVertical.Add(0, "0");
        _placesVertical.Add(1, "A");
        _placesVertical.Add(2, "B");
        _placesVertical.Add(3, "C");
        _placesVertical.Add(4, "D");
        _placesVertical.Add(5, "e");
        _placesVertical.Add(6, "f");
        _placesVertical.Add(7, "f");
        _placesVertical.Add(8, "f");

        for (int i = _map.GetHeight() - 1; i > 0;)
        {
            for (int j = 1; j < _map.GetHeight(); )
            {
                _placesHorizontal.Add(i, j);
                i--;
                j++;
            }
        }
    }

    public string GetTextPlace(int vertical, int horizontal)
    {
        string resultVertical = "0";
        string resultHorizontal = "n6";

        //if(_placesVertical.ContainsKey(vertical) && vertical == 5)
        //    return _placesVertical[vertical].ToString();

        if (_placesVertical.ContainsKey(vertical))
            resultVertical = _placesVertical[vertical].ToString();

        if (_placesHorizontal.ContainsKey(horizontal))
            resultHorizontal = _placesHorizontal[horizontal].ToString();

        string result = resultVertical + resultHorizontal;

        return result;
    }

}