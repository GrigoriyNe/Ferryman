using System;
using UnityEngine;
using UnityEngine.EventSystems;
using YG;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private Canvas _canvasNotMoving;
    [SerializeField] private float _mobileStartOffsetY = 2f;
    [SerializeField] private float _sensetivity;
    [SerializeField] private float _zoomFactor = 1.5f;

    private float _yMinValue = 7.8f;
    private float _yMinValueMobile = 8.2f;
    private float _yMaxValue = 15f;
    private float _xMinValue = 2f;
    private float _xMaxValue = 3f;
    private int _mobileSensetivityDivider = 2;

    private void Awake()
    {
        if (YG2.envir.isMobile)
        {
            Vector3 mobileCameraPosition = new Vector3(
                _cameraTransform.position.x,
                _cameraTransform.position.y + _mobileStartOffsetY,
                _cameraTransform.position.z);

            _yMinValue = _yMinValueMobile;
            _cameraTransform.position = mobileCameraPosition;
            _sensetivity = _sensetivity / _mobileSensetivityDivider;
        }
    }

    public void DragMove(Vector2 direction)
    {
        DragMovig(direction);
    }

    public void Zoom()
    {
        Vector3 target = new Vector3(
            _cameraTransform.position.x,
            _cameraTransform.position.y + _zoomFactor,
            _cameraTransform.position.z);

        _cameraTransform.position = target;
    }

    private void DragMovig(Vector2 direction)
    {
        Vector3 target = new Vector3(
            Math.Clamp(
                _cameraTransform.position.x - (direction.x * _sensetivity),
                _xMinValue, _xMaxValue),
            _cameraTransform.position.y,
            Math.Clamp(
                _cameraTransform.position.z - (direction.y * _sensetivity),
                _yMinValue, _yMaxValue));

        if (_canvasNotMoving.gameObject.activeSelf)
            if (EventSystem.current.IsPointerOverGameObject())
                return;

        _cameraTransform.position = target;
    }
}
