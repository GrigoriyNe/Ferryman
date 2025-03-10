using UnityEngine;

public class ObstacleView : MonoBehaviour
{
    [SerializeField] private ObstaclesSprites _spritesClosePark;
    [SerializeField] private ObstaclesSprites _spritesOpenPark;
    [SerializeField] private ObstaclesSprites _spritesEmpty;
    [SerializeField] private ObstaclesSprites _spritesOpenParkSpesial;
    [SerializeField] private ObstaclesSprites _spritesCloseParkSpesial;

    public Sprite GetSpriteClose()
    {
        return _spritesClosePark.GetSprite(); 
    }

    public Sprite GetSpriteOpen()
    {
        return _spritesOpenPark.GetSprite();
    }

    public Sprite GetSpriteOpenSpesial()
    {
        return _spritesOpenParkSpesial.GetSprite();
    }
    public Sprite GetSpriteCloseSpesial()
    {
        return _spritesCloseParkSpesial.GetSprite();
    }

    public Sprite GetSpriteEmpty()
    {
        return _spritesEmpty.GetSprite();
    }
}
