using TMPro;
using UnityEngine;

public class LevelView : MonoBehaviour
{
    [SerializeField] private Game _game;
    [SerializeField] private TextMeshProUGUI _text;

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
        string result = (level + 1).ToString();
        _text.text = result;
    }
}