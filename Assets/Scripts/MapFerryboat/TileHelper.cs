using TMPro;
using UnityEngine;

public class TileHelper : SpawnableObject
{
    [SerializeField] private TextMeshProUGUI _rewardView;

    public int cordX;
    public int cordY;
    public SpriteRenderer spriteRenderer;

    public GameObject[] walls;
    public bool[] wallsBool;

    private int _rewardValue = 0;

    public int Reward => _rewardValue;

    private void OnDisable()
    {
        _rewardValue = 0;
        _rewardView.text = "";
    }

    public void SetRewardValue(int value)
    {
        _rewardValue = value;

        if (value > 0)
        {
            _rewardView.color = Color.yellow;
            _rewardView.text = value.ToString();
        }
        else
        {
            _rewardView.color = Color.red;
            _rewardView.text = value.ToString();
        }
    }

    public void SetWalls(int walls_int)
    {
        wallsBool[walls_int] = true;
        walls[walls_int].SetActive(true);
    }

    public void RemoveWalls()
    {
        for (int i = 0; i < walls.Length - 1; i++)
        {
            wallsBool[i] = false;
            walls[i].SetActive(false);
        }
    }
}
