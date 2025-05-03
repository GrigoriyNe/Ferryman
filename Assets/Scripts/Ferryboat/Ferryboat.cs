using System.Collections;
using UnityEngine;
using MapFerryboat;
using CarGroup;

namespace FerryboatGroup
{
    public class Ferryboat : MonoBehaviour
    {
        private const int ActivateDelayValue = 3;
        private const int DeactivateDelayValue = 1;

        [SerializeField] private Game.GameProcess _game;
        [SerializeField] private Map _map;
        [SerializeField] private BackgoundTextPlaces _numberingText;
        [SerializeField] private Envoriments.WindowBlind _blind;
        [SerializeField] private FerryboatAnimator _animator;
        [SerializeField] private Namer _places;

        private WaitForSeconds _waitActivating;
        private WaitForSeconds _waitDeactivating;

        private void OnEnable()
        {
            _waitActivating = new WaitForSeconds(ActivateDelayValue);
            _waitDeactivating = new WaitForSeconds(DeactivateDelayValue);
        }

        public void Activate()
        {
            StartCoroutine(Activating());
        }

        public void Finish()
        {
            StartCoroutine(Deactivating());
        }

        public Map GetMap()
        {
            return _map;
        }

        public Namer GetPlaces()
        {
            return _places.Get();
        }

        private IEnumerator Activating()
        {
            _animator.PlayStart();
            yield return _waitActivating;
            _blind.Open();
            _map.Activate();
            _numberingText.Activate();
        }

        private IEnumerator Deactivating()
        {
            _map.Deactivate();
            _blind.Close();

            yield return _waitDeactivating;

            _animator.PlayFinish();
            _numberingText.Deactivate();
        }
    }
}