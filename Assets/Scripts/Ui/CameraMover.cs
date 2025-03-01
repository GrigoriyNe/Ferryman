using System;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private CameraButtonsChange _botton;

    private void OnEnable()
    {
        _botton.ButtonClicked += OnUpClick;
        _botton.ButtonZoomClicked += OnZoomClick;
    }

    private void OnDisable()
    {
        _botton.ButtonClicked -= OnUpClick;
        _botton.ButtonZoomClicked -= OnZoomClick;
    }

    private void OnZoomClick(int value)
    {
        float valueY = Math.Clamp((_cameraTransform.position.y + value), 3f, 15f);
        _cameraTransform.position = new Vector3(_cameraTransform.position.x, valueY, _cameraTransform.position.z);
    }

    private void OnUpClick(int value)
    {
        float valueZ = Math.Clamp((_cameraTransform.position.z + value), 2f, 10f);
        _cameraTransform.position = new Vector3(_cameraTransform.position.x, _cameraTransform.position.y, valueZ);
    }
}
