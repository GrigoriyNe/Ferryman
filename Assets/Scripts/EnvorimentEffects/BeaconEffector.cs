using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Envoriments
{
    public class BeaconEffector : MonoBehaviour
    {
        private const float MinValueSecond = 1.0f;
        private const float Speed = 10;

        [SerializeField] private Game.GameLoop _game;
        [SerializeField] private List<Transform> _beaconsLeft;
        [SerializeField] private List<Transform> _beaconsRigth;
        [SerializeField] private float _deepFactor = 1f;
        [SerializeField] private float _maxValueSecond = 3f;

        private void OnEnable()
        {
            _game.StartSceneStart += StartSceneStart;
            _game.StartSceneDone += OnStartSceneDone;
            _game.FinishSceneStart += OnFinishSceneStart;
            _game.FinishSceneDone += OnFinishSceneDone;

            StartCoroutine(UpBeacon(_beaconsRigth));
        }

        private void OnDisable()
        {
            _game.StartSceneStart -= StartSceneStart;
            _game.StartSceneDone -= OnStartSceneDone;
            _game.FinishSceneStart -= OnFinishSceneStart;
            _game.FinishSceneDone -= OnFinishSceneDone;
        }

        private void StartSceneStart()
        {
            DownBeacon(_beaconsLeft);
        }

        private void OnStartSceneDone()
        {
            StartCoroutine(UpBeacon(_beaconsLeft));
        }

        private void OnFinishSceneStart()
        {
            DownBeacon(_beaconsRigth);
        }

        private void OnFinishSceneDone()
        {
            StartCoroutine(UpBeacon(_beaconsRigth));
        }

        private IEnumerator RandomisingBeacon()
        {
            float randomWaitValue = UnityEngine.Random.Range(MinValueSecond, _maxValueSecond);

            foreach (Transform beacon in _beaconsRigth)
            {
                yield return new WaitForSeconds(randomWaitValue);
                beacon.gameObject.SetActive(true);
            }
        }

        private IEnumerator UpBeacon(List<Transform> beacons)
        {
            float randomWaitValue = UnityEngine.Random.Range(0f, _maxValueSecond);

            foreach (Transform beacon in beacons)
            {
                yield return new WaitForSeconds(randomWaitValue);
                Vector3 target = new Vector3(beacon.position.x, beacon.position.y + _deepFactor, beacon.position.z);
                StartCoroutine(Moving(beacon, target, true));
            }
        }

        private void DownBeacon(List<Transform> beacons)
        {
            foreach (Transform beacon in beacons)
            {
                Vector3 target = new Vector3(beacon.position.x, beacon.position.y - _deepFactor, beacon.position.z);
                StartCoroutine(Moving(beacon, target, false));
            }
        }

        private IEnumerator Moving(Transform beacon, Vector3 target, bool isActive)
        {
            if (isActive)
                beacon.gameObject.SetActive(isActive);

            while (beacon.position != target)
            {
                beacon.position = Vector3.MoveTowards(beacon.position, target, Time.deltaTime * Speed);
                yield return null;
            }

            if (isActive == false)
                beacon.gameObject.SetActive(isActive);
        }
    }
}