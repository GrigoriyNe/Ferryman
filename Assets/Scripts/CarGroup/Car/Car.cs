using UnityEngine;
using TileGroup;

namespace CarGroup
{
    public class Car : MonoBehaviour
    {
        [SerializeField] private CarMover _mover;
        [SerializeField] private CarTextViewer _textViewer;
        [SerializeField] private MeshFilter _body;
        [SerializeField] private MeshFilter _supercharger;

        public void Init(Color bodyColor, Tile startPositionTile, Tile finishPositionTile, Namer parkPlace)
        {
            _body.GetComponent<MeshRenderer>().material.color = bodyColor;
            _supercharger.GetComponent<MeshRenderer>().material.color = bodyColor;
            _mover.Init(startPositionTile, finishPositionTile, parkPlace);
            _textViewer.Init(parkPlace, finishPositionTile);
            transform.position = startPositionTile.transform.position;

            _mover.MoveInQuenue();
        }

        public void Move()
        {
            _mover.Move();
        }

        public void MoveAway()
        {
            _mover.MoveAway();
        }
    }
}