using TMPro;
using UnityEngine;

public class LevelView : MonoBehaviour
{
    private const int ViewOffset = 1;

    [SerializeField] private Game _game;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private AnimationResources _animator;

    private void OnEnable()
    {
        _game.LevelChange += OnLevelChange;
    }

    private void OnDisable()
    {
        _game.LevelChange -= OnLevelChange;
    }

    private void OnLevelChange(int level)
    {
        string result = (level + ViewOffset).ToString();
        _text.text =  result;
        _animator.ActivateLevelUi();
    }
}
