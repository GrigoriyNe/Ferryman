using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TileGroup;

namespace CarGroup
{
    public class CarTextViewer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _viewFinishPosition;
        [SerializeField] private Image _backgroundImage;

        private Namer _parkPlace;
        private Tile _finishPositionTile;

        public void Init(Namer parkPlace, Tile finishPositionTile)
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
            string result = _parkPlace.GetTextPlace(_finishPositionTile.CordX, _finishPositionTile.CordY).ToString();
            return result;
        }
    }
}