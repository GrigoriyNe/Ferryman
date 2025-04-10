using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TileHelper : SpawnableObject
{
    [SerializeField] private TextMeshProUGUI _rewardView;
    [SerializeField] private Sprite _defaultSprite;
    [SerializeField] private Image _ligter;

    public int cordX;
    public int cordY;
    public SpriteRenderer spriteRenderer;

    public GameObject[] walls;
    public bool[] wallsBool;

    private int _rewardValue = 0;

    public int Reward => _rewardValue;

    private float _offsetY = 0.57f;


    private void OnDisable()
    {
        spriteRenderer.sprite = _defaultSprite;
        spriteRenderer.transform.position = new Vector3(spriteRenderer.transform.position.x, 0, spriteRenderer.transform.position.z);
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

    public void SetWinnerState()
    {
        spriteRenderer.transform.position = new Vector3(spriteRenderer.transform.position.x, _offsetY, spriteRenderer.transform.position.z);
        _rewardView.color = Color.green;
    }

    public void SetDefaultState()
    {
        spriteRenderer.transform.position = new Vector3 (spriteRenderer.transform.position.x, 0, spriteRenderer.transform.position.z);
        _rewardView.color = Color.yellow;
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

    public void DectivateLigther()
    {
        _ligter.gameObject.SetActive(false);
    }

    public void ActivateLigther()
    {
        _ligter.gameObject.SetActive(true);
    }
}
