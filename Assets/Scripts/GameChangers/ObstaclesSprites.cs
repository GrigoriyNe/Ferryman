using System;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesSprites : MonoBehaviour
{
    [SerializeField] private List<Sprite> _sprites = new List<Sprite>();
    bool _isEven = true;

    public Sprite GetSprite()
    {
        _isEven = !_isEven;

        int result = Convert.ToInt32(_isEven);

        return _sprites[result];
    }
}
