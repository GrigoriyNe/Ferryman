using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _lookSpeed;
    [SerializeField] private Camera _camera;

    private PlayerInput _input;

    private Vector2 _tapPoint;

    public event Action<int, int> Clicked;

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

                    if (hit.collider.TryGetComponent(out TileHelper tile))
                    {
                        Clicked?.Invoke(tile.cordX, tile.cordY);
                    }

                    //Clicked?.Invoke(hit);
                    context = new InputAction.CallbackContext();
                    _input.Player.Click.performed -= OnClick;
                    StartCoroutine(Cooldown());
                }
            }
        }
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(0.5f);

        _input.Player.Click.performed += OnClick;
    }
}
