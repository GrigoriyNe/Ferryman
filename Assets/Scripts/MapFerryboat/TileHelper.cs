using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class TileHelper : SpawnableObject
{
    [SerializeField] private Sprite _defaultSprite;
    [SerializeField] private Image _ligter;
    [SerializeField] private RewardView _rewardView;

    public int cordX;
    public int cordY;
    public SpriteRenderer spriteRenderer;

    public GameObject[] walls;
    public bool[] wallsBool;

    private int _rewardValue = 0;

    public int Reward => _rewardValue;

    private float _offsetY = 0f;
    private float _offsetYObstacle = 0.11f;
    private float _offsetYWinState = .8f;

    private Vector3 _defaultScale = new Vector3(0.4f, 0.4f, 0.4f);
    private Vector3 _winScale = new Vector3(0.3f, 0.3f, 0.3f);

    private void OnEnable()
    {
        spriteRenderer.transform.localScale = _defaultScale;
    }

    private void OnDisable()
    {
        spriteRenderer.sprite = _defaultSprite;
        spriteRenderer.transform.localScale = _defaultScale;
        spriteRenderer.transform.position = new Vector3(spriteRenderer.transform.position.x, _offsetY, spriteRenderer.transform.position.z);
        _rewardValue = 0;
    }

    public void SetRewardValue(int value)
    {
        _rewardValue = value;

        if (value > 0)
        {
            spriteRenderer.sprite = _rewardView.GetNeturalView(value);
            spriteRenderer.transform.position = new Vector3(spriteRenderer.transform.position.x, _offsetY, spriteRenderer.transform.position.z);
            //   _rewardView.color = Color.yellow;
            //   _rewardView.text = value.ToString();
        }
        else
        {
            spriteRenderer.sprite = _rewardView.GetNegativeiveView(value);
            spriteRenderer.transform.position = new Vector3(spriteRenderer.transform.position.x, _offsetYObstacle, spriteRenderer.transform.position.z);
            //    _rewardView.color = Color.red;
            //    _rewardView.text = value.ToString();
        }
    }

    public void SetWinnerState()
    {
        spriteRenderer.sprite = _rewardView.GetPositiveView(_rewardValue);
        spriteRenderer.transform.localScale = _winScale;
        spriteRenderer.transform.position = new Vector3(spriteRenderer.transform.position.x, _offsetYWinState, spriteRenderer.transform.position.z);
        //     _rewardView.color = Color.green;
    }

    public void SetDefaultState()
    {
        spriteRenderer.sprite = _rewardView.GetNeturalView(_rewardValue);
        spriteRenderer.transform.localScale = _defaultScale;
        spriteRenderer.transform.position = new Vector3(spriteRenderer.transform.position.x, _offsetY, spriteRenderer.transform.position.z);
        //     _rewardView.color = Color.yellow;
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
