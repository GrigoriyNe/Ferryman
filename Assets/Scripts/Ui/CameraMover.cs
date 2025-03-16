using System;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;

    public void DragMove(Vector2 direction)
    {
        float time = Time.deltaTime;
        float vertical = 0f;
        float horizontal = 0f;

        vertical += direction.y * time;
        horizontal += direction.x * time;
        Math.Clamp((vertical), 2f, 10f);
        Math.Clamp((horizontal), 2f, 4f);

        _cameraTransform.position = new Vector3(
            Math.Clamp((_cameraTransform.position.x - horizontal), 2f, 5f),
            _cameraTransform.position.y,
            Math.Clamp((_cameraTransform.position.z - vertical), 2f, 10f));
    }
}
