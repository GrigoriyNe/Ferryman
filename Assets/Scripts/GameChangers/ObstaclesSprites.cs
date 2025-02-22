using System.Collections.Generic;
using UnityEngine;

public class ObstaclesSprites : MonoBehaviour
{
    [SerializeField] private List<Sprite> _sprites = new List<Sprite>();

    public Sprite GetSprite()
    {
        return _sprites[Random.Range(0, _sprites.Count)];
    }
}
