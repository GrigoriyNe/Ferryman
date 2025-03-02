using System;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private CameraButtonsChange _botton;

    private void OnEnable()
    {
        _botton.ButtonVerticalClicked += OnUpClick;
        _botton.ButtonZoomClicked += OnZoomClick;
        _botton.ButtonHorizontalClicked += OnHorizontalClick;
    }

    private void OnDisable()
    {
        _botton.ButtonVerticalClicked -= OnUpClick;
        _botton.ButtonZoomClicked -= OnZoomClick;
        _botton.ButtonHorizontalClicked -= OnHorizontalClick;
    }

    private void OnHorizontalClick(int value)
    {
        float valueX = Math.Clamp((_cameraTransform.position.x + value), 2f, 4f);
        _cameraTransform.position = new Vector3(valueX, _cameraTransform.position.y, _cameraTransform.position.z);
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
