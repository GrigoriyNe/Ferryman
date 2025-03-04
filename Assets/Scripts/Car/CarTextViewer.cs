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
    private WaitForSeconds _wait;
    private Coroutine _activating = null;

    private void Start()
    {
        _wait = new WaitForSeconds(Delay);
    }

    public void Init(Namer parkPlace, TileHelper finishPositionTile)
    {
        _parkPlace = parkPlace;
        _finishPositionTile = finishPositionTile;
        _viewFinishPosition.text = GetTextPosition();

        _activating = StartCoroutine(SmoothActivate());
    }

    public void ActivateBackground()
    {
        _backgroundImage.gameObject.SetActive(true);
    }

    public void DeactivateBackground()
    {
        if (_activating != null)
            _activating = null;

        _backgroundImage.gameObject.SetActive(false);
    }

    private string GetTextPosition()
    {
        string result = _parkPlace.GetTextPlace(_finishPositionTile.cordX, _finishPositionTile.cordY).ToString();
        return result;
    }

    private IEnumerator SmoothActivate()
    {
        yield return _wait;

        ActivateBackground();
    }
}