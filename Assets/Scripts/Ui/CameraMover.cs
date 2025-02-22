using System;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private CameraButtonUp _up;
    [SerializeField] private CameraButtonDown _down;

    private void OnEnable()
    {
        _up.ButtonClicked += OnUpClick;
        _down.ButtonClicked += OnDownClick;
    }

    private void OnDisable()
    {
        _up.ButtonClicked -= OnUpClick;
        _down.ButtonClicked -= OnDownClick;
    }

    private void OnDownClick()
    {
        float valueZ = Math.Clamp((_cameraTransform.position.z - 1), 1f, 10f);
        _cameraTransform.position = new Vector3(_cameraTransform.position.x, _cameraTransform.position.y, valueZ);
    }

    private void OnUpClick()
    {
        float valueZ = Math.Clamp((_cameraTransform.position.z + 1), 1f, 10f);
        _cameraTransform.position = new Vector3(_cameraTransform.position.x, _cameraTransform.position.y, valueZ);
    }
}
