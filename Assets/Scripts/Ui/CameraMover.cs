using System;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private float _sensetivity = 1f;
    [SerializeField] private float _zoomFactor = 1.5f;

    public void DragMove(Vector2 direction)
    {
        float time = Time.deltaTime;
        float vertical = 0f;
        float horizontal = 0f;

        vertical += direction.y * time * _sensetivity;
        horizontal += direction.x * time * _sensetivity;
        Vector3 target = new Vector3(
            Math.Clamp((_cameraTransform.position.x - horizontal), 2f, 5f),
            _cameraTransform.position.y,
            Math.Clamp((_cameraTransform.position.z - vertical), 2f, 15f));

        _cameraTransform.position = target;
    }

    public void Zoom()
    {
        Vector3 target = new Vector3(_cameraTransform.position.x, _cameraTransform.position.y + _zoomFactor, _cameraTransform.position.z);
        _cameraTransform.position = target;
    }
}
