using System.Collections.Generic;
using UnityEngine;

public class NamesOfParkingPlacesFirs : Namer
{
    [SerializeField] Map _map;

    private Dictionary<int, string> _placesVertical;
    private Dictionary<int, int> _placesHorizontal;

    public void OnEnable()
    {
        _placesVertical = new Dictionary<int, string>();
        _placesHorizontal = new Dictionary<int, int>();
        FillDictionary();
    }

    public void FillDictionary()
    {
        _placesVertical.Add(0, "A");
        _placesVertical.Add(1, "B");
        _placesVertical.Add(2, "C");
        _placesVertical.Add(3, "D");
        _placesVertical.Add(4, "e");

        int height = _map.GetHeight();

        for (int i = height - 4; i < height;)
        {
            for (int j = 1; j <= (height - 4);)
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
            return _placesVertical[vertical].ToString();

        if (_placesVertical.ContainsKey(vertical))
            resultVertical = _placesVertical[vertical].ToString();

        if (_placesHorizontal.ContainsKey(horizontal))
            resultHorizontal = _placesHorizontal[horizontal].ToString();

        string result = resultVertical + resultHorizontal;

        return result;
    }
}