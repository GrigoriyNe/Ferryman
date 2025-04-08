using UnityEngine;

public class ObstaclesSprites : MonoBehaviour
{
    [SerializeField] private Sprite _sprite;

    public Sprite GetSprite()
    {
        return _sprite;
    }
}
