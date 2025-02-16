using System.Collections.Generic;
using UnityEngine;

public class NamesOfParkingPlaces : MonoBehaviour
{
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
        _placesVertical.Add(1, "A");
        _placesVertical.Add(2, "B");
        _placesVertical.Add(3, "C");
        _placesVertical.Add(4, "D");
        _placesVertical.Add(5, "e");

        for (int i = 14; i > 11;)
        {
            for (int j = 1; j < 5; j++)
            {
                _placesHorizontal.Add(i, j);
                i--;
            }
        }
    }

    public string GetTextPlace(int vertical, int horizontal)
    {
        string resultVertical = string.Empty;
        string resultHorizontal = string.Empty;

        if(_placesVertical.ContainsKey(vertical) && vertical == 5)
            return _placesVertical[vertical].ToString();

        if (_placesVertical.ContainsKey(vertical))
            resultVertical = _placesVertical[vertical].ToString();
        if (_placesHorizontal.ContainsKey(horizontal))
            resultHorizontal = _placesHorizontal[horizontal].ToString();

        string result = resultVertical + resultHorizontal;

        return result;
    }

}