using UnityEngine;
using UnityEngine.UI;

public class TileHelper : SpawnableObject
{
    [SerializeField] private Sprite _defaultSprite;
    [SerializeField] private SpriteRenderer _ligter;
    [SerializeField] private RewardView _rewardView;
    [SerializeField] private SpriteRenderer _rewardSpriteRenderer;
    [SerializeField] private GameObject[] walls;
    [SerializeField] private bool[] wallsBool;

    private int _rewardValue = 0;
    private float _offsetDefaultY = 0f;
    private float _offsetObstacleY = 0.11f;
    private float _offsetWinStateY = .8f;

    private Vector3 _defaultScale = new Vector3(0.4f, 0.4f, 0.4f);
    private Vector3 _winScale = new Vector3(0.3f, 0.3f, 0.3f);

    public int CordX { get; private set; }
    public int CordY { get; private set; }

    public int Reward => _rewardValue;

    private void OnEnable()
    {
        _rewardSpriteRenderer.transform.localScale = _defaultScale;
    }

    private void OnDisable()
    {
        _rewardSpriteRenderer.sprite = _defaultSprite;
        _rewardSpriteRenderer.transform.localScale = _defaultScale;
        _rewardSpriteRenderer.transform.position = new Vector3(_rewardSpriteRenderer.transform.position.x, _offsetDefaultY, _rewardSpriteRenderer.transform.position.z);
        _rewardValue = 0;
        _rewardSpriteRenderer.gameObject.SetActive(false);
    }

    public void ChangeX(int value)
    {
        CordX = value;
    }

    public void ChangeY(int value)
    {
        CordY = value;
    }

    public void SetRewardValue(int value)
    {
        _rewardValue = value;
        _rewardSpriteRenderer.gameObject.SetActive(true);

        if (value > 0)
        {
            _rewardSpriteRenderer.sprite = _rewardView.GetNeturalView(value);
            _rewardSpriteRenderer.transform.position = new Vector3(_rewardSpriteRenderer.transform.position.x,
                _offsetDefaultY,
                _rewardSpriteRenderer.transform.position.z);
        }
        else
        {
            _rewardSpriteRenderer.sprite = _rewardView.GetNegativeiveView(value);
            _rewardSpriteRenderer.transform.position = new Vector3(_rewardSpriteRenderer.transform.position.x,
                _offsetObstacleY,
                _rewardSpriteRenderer.transform.position.z);
        }
    }

    public void SetWinnerState()
    {
        _rewardSpriteRenderer.sprite = _rewardView.GetPositiveView(_rewardValue);
        _rewardSpriteRenderer.transform.localScale = _winScale;
        _rewardSpriteRenderer.transform.position = new Vector3(
            _rewardSpriteRenderer.transform.position.x,
            _offsetWinStateY,
            _rewardSpriteRenderer.transform.position.z);
    }

    public void SetDefaultState()
    {
        _rewardSpriteRenderer.sprite = _rewardView.GetNeturalView(_rewardValue);
        _rewardSpriteRenderer.transform.localScale = _defaultScale;
        _rewardSpriteRenderer.transform.position = new Vector3(
            _rewardSpriteRenderer.transform.position.x,
            _offsetDefaultY,
            _rewardSpriteRenderer.transform.position.z);
    }

    public void SetWalls(int walls)
    {
        wallsBool[walls] = true;
        this.walls[walls].SetActive(true);
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
        _ligter.gameObject.gameObject.SetActive(false);
    }

    public void ActivateLigther()
    {
        _ligter.gameObject.SetActive(true);
    }
}
