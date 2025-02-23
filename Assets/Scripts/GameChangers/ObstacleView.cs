using UnityEngine;

public class ObstacleView : MonoBehaviour
{
    [SerializeField] private ObstaclesSprites _spritesClosePark;
    [SerializeField] private ObstaclesSprites _spritesOpenPark;

    public Sprite GetSpriteClose()
    {
        return _spritesClosePark.GetSprite(); 
    }

    public Sprite GetSpriteOpen()
    {
        return _spritesOpenPark.GetSprite();
    }
}
