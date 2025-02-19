using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CarTextViewer : MonoBehaviour
{
    private const int Delay = 3;

    [SerializeField] private TextMeshProUGUI _viewFinishPosition;
    [SerializeField] private Image _backgroundImage;

    private Namer _parkPlace;
    private TileHelper _finishPositionTile;
    private WaitForSeconds _wait;

    private void OnEnable()
    {
        DeactivateBackground();
        _wait = new WaitForSeconds(Delay);
    }

    private void OnDisable()
    {
        ActivateBackground();
    }

    public void Init(Namer parkPlace, TileHelper finishPositionTile)
    {
        _parkPlace = parkPlace;
        _finishPositionTile = finishPositionTile;
        _viewFinishPosition.text = GetTextPosition();

        StartCoroutine(SmoothActivate());
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
        return _parkPlace.GetTextPlace(_finishPositionTile.cord_x, _finishPositionTile.cord_y).ToString();
    }

    private IEnumerator SmoothActivate()
    {
        yield return new WaitForSeconds(Delay);

        ActivateBackground();
    }
}