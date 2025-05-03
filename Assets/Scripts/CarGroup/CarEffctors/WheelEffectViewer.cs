using UnityEngine;

namespace CarGroup
{
    public class WheelEffectViewer : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineOfFTrail;

        private void OnEnable()
        {
            _lineOfFTrail.positionCount = 0;
        }

        private void OnDisable()
        {
            _lineOfFTrail.positionCount = 0;
        }

        public void DrawLine()
        {
            _lineOfFTrail.SetPosition(_lineOfFTrail.positionCount++, transform.position);
        }
    }
}