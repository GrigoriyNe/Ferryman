using System;
using System.Collections;
using UnityEngine;
using YG;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private float _mobileOffsetY = 3f;
    [SerializeField] private float _sensetivity;
    [SerializeField] private float _zoomFactor = 1.5f;

    private void Awake()
    {
        if (YandexGame.EnvironmentData.isMobile == true)
        {
            Vector3 mobileCameraPosition = new Vector3(
                _cameraTransform.position.x,
                _cameraTransform.position.y + _mobileOffsetY,
                _cameraTransform.position.z);

            _cameraTransform.position = mobileCameraPosition;
        }

        _sensetivity = _sensetivity / 2;
    }

    public void DragMove(Vector2 direction)
    {
        DragMovig(direction);
    }

    public void Zoom()
    {
        Vector3 target = new Vector3(_cameraTransform.position.x, _cameraTransform.position.y + _zoomFactor, _cameraTransform.position.z);
        _cameraTransform.position = target;
    }

    private void DragMovig(Vector2 direction)
    {
        float step = Time.deltaTime;

        Vector3 target = new Vector3(
            Math.Clamp((_cameraTransform.position.x - (direction.x * _sensetivity)), 2f, 5f),
            _cameraTransform.position.y,
            Math.Clamp((_cameraTransform.position.z - (direction.y * _sensetivity)), 7.8f, 15f));

        _cameraTransform.position = target;
    }
}
