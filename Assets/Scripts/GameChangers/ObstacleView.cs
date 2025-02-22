using UnityEngine;

public class ObstacleView : MonoBehaviour
{
    [SerializeField] private ObstaclesSprites _sprites;

    public Sprite GetSprite()
    {
        return _sprites.GetSprite(); 
    }
}
