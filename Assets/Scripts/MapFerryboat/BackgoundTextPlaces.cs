using UnityEngine;

namespace MapFerryboat
{
    public class BackgoundTextPlaces : MonoBehaviour
    {
        [SerializeField] private GameObject _boxWihtText;

        private void Start()
        {
            _boxWihtText.SetActive(false);
        }

        public void Activate()
        {
            _boxWihtText.SetActive(true);
        }

        public void Deactivate()
        {
            _boxWihtText.SetActive(false);
        }
    }
}