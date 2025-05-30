using System;
using UnityEngine;

namespace TileGroup
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] private Sprite _defaultSprite;
        [SerializeField] private SpriteRenderer _ligter;
        [SerializeField] private SpriteRenderer _rewardSpriteRenderer;
        [SerializeField] private Counters.RewardView _rewardView;
        [SerializeField] private GameObject[] walls;
        [SerializeField] private bool[] wallsBool;

        private int _rewardValue = 0;
        private float _offsetDefaultY = 0f;
        private float _offsetObstacleY = 0.11f;
        private float _offsetWinStateY = .8f;

        private Vector3 _defaultScale = new Vector3(0.4f, 0.4f, 0.4f);
        private Vector3 _winScale = new Vector3(0.3f, 0.3f, 0.3f);

        public enum Type
        {
            Regular,
            Special,
        }

        public Type TileType { get; private set; }

        public Vector2 Coords { get; private set; }

        public int CordY => Convert.ToInt16(Coords.y);

        public int CordX => Convert.ToInt16(Coords.x);

        public int Reward => _rewardValue;

        public bool IsSpesial { get; private set; }

        private void OnEnable()
        {
            _rewardSpriteRenderer.transform.localScale = _defaultScale;
        }

        private void OnDisable()
        {
            _rewardSpriteRenderer.sprite = _defaultSprite;
            _rewardSpriteRenderer.transform.localScale = _defaultScale;

            TranlateTo(_offsetDefaultY);
            _rewardValue = 0;
            _rewardSpriteRenderer.gameObject.SetActive(false);
        }

        public void ChangeType(Type type)
        {
            TileType = type;
        }

        public void ChangeTilePlace(Vector2 value)
        {
            Coords = value;
        }

        public void SetRewardValue(int value)
        {
            _rewardValue = value;
            _rewardSpriteRenderer.gameObject.SetActive(true);

            if (value > 0)
            {
                _rewardSpriteRenderer.sprite = _rewardView.GetNeturalView(value);
                TranlateTo(_offsetDefaultY);
            }
            else
            {
                _rewardSpriteRenderer.sprite = _rewardView.GetNegativeiveView(value);
                TranlateTo(_offsetObstacleY);
            }
        }

        public void SetWinnerState()
        {
            _rewardSpriteRenderer.sprite = _rewardView.GetPositiveView(_rewardValue);
            TranlateTo(_offsetWinStateY);
            _rewardSpriteRenderer.transform.localScale = _winScale;
        }

        public void SetDefaultState()
        {
            _rewardSpriteRenderer.sprite = _rewardView.GetNeturalView(_rewardValue);
            TranlateTo(_offsetDefaultY);
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

        public void ChangeLigther(bool isActive)
        {
            _ligter.gameObject.gameObject.SetActive(isActive);
        }

        private void TranlateTo(float heigth)
        {
            _rewardSpriteRenderer.transform.localScale = _defaultScale;
            _rewardSpriteRenderer.transform.position = new Vector3(
                _rewardSpriteRenderer.transform.position.x,
                heigth,
                _rewardSpriteRenderer.transform.position.z);
        }
    }
}