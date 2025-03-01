using UnityEngine;

public class TileHelper : MonoBehaviour
{
    public int cordX;
    public int cordY;
    public SpriteRenderer spriteRenderer;

    public GameObject[] walls;
    public bool[] wallsBool;

    public void SetWalls(int walls_int)
    {
        wallsBool[walls_int] = true;
        walls[walls_int].SetActive(true);
    }
}
