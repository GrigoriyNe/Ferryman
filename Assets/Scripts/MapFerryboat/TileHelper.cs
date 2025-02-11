using UnityEngine;

public class TileHelper : MonoBehaviour
{
    public int cord_x;
    public int cord_y;
    public SpriteRenderer sprite;

    public GameObject[] walls;
    public bool[] wallsBool;

    public void SetWalls(int walls_int)
    {
        wallsBool[walls_int] = true;
        walls[walls_int].SetActive(true);
    }
}
