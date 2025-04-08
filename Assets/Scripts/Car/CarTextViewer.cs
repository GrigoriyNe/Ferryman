using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CarTextViewer : MonoBehaviour
{
    private const int Delay = 2;

    [SerializeField] private TextMeshProUGUI _viewFinishPosition;
    [SerializeField] private Image _backgroundImage;

    private Namer _parkPlace;
    private TileHelper _finishPositionTile;

    public void Init(Namer parkPlace, TileHelper finishPositionTile)
    {
        _parkPlace = parkPlace;
        _finishPositionTile = finishPositionTile;
        _viewFinishPosition.text = GetTextPosition();

        ActivateBackground();
    }

    public void ActivateBackground()
    {
        _backgroundImage.gameObject.SetActive(true);
    }

    public void DeactivateBackground()
    {
        _backgroundImage.gameObject.SetActive(false);
    }

    private string GetTextPosition()
    {
        string result = _parkPlace.GetTextPlace(_finishPositionTile.cordX, _finishPositionTile.cordY).ToString();
        return result;
    }
}