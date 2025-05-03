using System.Collections.Generic;
using UnityEngine;

public class NamesOfParkingPlacesThird : Namer
{
    [SerializeField] private Map _map;

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
        _placesVertical.Add(0, "g");
        _placesVertical.Add(1, "A");
        _placesVertical.Add(2, "B");
        _placesVertical.Add(3, "C");
        _placesVertical.Add(4, "D");
        _placesVertical.Add(5, "f");

        int height = _map.GetHeight();

        for (int i = height - 6; i < height;)
        {
            for (int j = 1; j <= (height - 6);)
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

        if (_placesVertical.ContainsKey(vertical) && vertical == 0)
        {
            if (_placesHorizontal.ContainsKey(horizontal) && horizontal == 22)
                return "f";

            if (_placesHorizontal.ContainsKey(horizontal) && horizontal == 20)
                return "h";

            if (_placesHorizontal.ContainsKey(horizontal) && horizontal == 18)
                return "j";
        }

        if (_placesVertical.ContainsKey(vertical) && vertical == 5)
        {
            if (_placesHorizontal.ContainsKey(horizontal) && horizontal == 22)
                return "g";

            if (_placesHorizontal.ContainsKey(horizontal) && horizontal == 20)
                return "i";

            if (_placesHorizontal.ContainsKey(horizontal) && horizontal == 18)
                return "k";
        }

        if (_placesVertical.ContainsKey(vertical))
            resultVertical = _placesVertical[vertical].ToString();

        if (_placesHorizontal.ContainsKey(horizontal))
            resultHorizontal = _placesHorizontal[horizontal].ToString();

        string result = resultVertical + resultHorizontal;

        return result;
    }
}