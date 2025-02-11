using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _lookSpeed;
    [SerializeField] private Camera _camera;

    private PlayerInput _input;

    private Vector2 _tapPoint;

    public event Action<RaycastHit> Clicked;

    private void OnEnable()
    {
        _input.Enable();
        _input.Player.Click.started += OnClick;
        _input.Player.Click.performed += OnClick;
        _input.Player.Click.canceled += OnClick;
    }

    private void OnDisable()
    {
        _input.Disable();
        _input.Player.Click.started -= OnClick;
        _input.Player.Click.performed -= OnClick;
        _input.Player.Click.canceled -= OnClick;
    }

    private void Awake()
    {
        _input = new PlayerInput();
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            _tapPoint = Vector2.zero;
        }
        else if (context.performed)
        {
            context.ReadValue<Vector2>();
            _tapPoint = context.ReadValue<Vector2>();

            if (_tapPoint != Vector2.zero)
            {
                Ray castPoint = Camera.main.ScreenPointToRay(_tapPoint);
                RaycastHit hit;

                if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
                {
                    if (hit.collider.TryGetComponent(out Car car))
                    {
                        car.Move();
                    }

                    //Clicked?.Invoke(hit);
                    context = new InputAction.CallbackContext();
                }
            }
        }
    }
}
