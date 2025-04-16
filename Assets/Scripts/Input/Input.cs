using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] private CameraMover _cameraMover;
    [SerializeField] private Soungs _soung;

    private PlayerInput _input;

    private Vector2 _tapPoint;
    private Vector2 _cameraDelta;

    public event Action<int, int> Clicked;

    private void OnEnable()
    {
        _input.Enable();
        _input.Player.Click.started += OnClick;
        _input.Player.Click.performed += OnClick;
        _input.Player.Click.canceled += OnClick;

        _input.Player.CameraMover.started += OnClickMover;
        _input.Player.CameraMover.performed += OnClickMover;
        _input.Player.CameraMover.canceled += OnClickMover;
    }

    private void OnDisable()
    {
        _input.Disable();
        _input.Player.Click.started -= OnClickMover;
        _input.Player.Click.performed -= OnClickMover;
        _input.Player.Click.canceled -= OnClickMover;

        _input.Player.CameraMover.started -= OnClickMover;
        _input.Player.CameraMover.performed -= OnClickMover;
        _input.Player.CameraMover.canceled -= OnClickMover;
    }

    private void Awake()
    {
        _input = new PlayerInput();
    }

    private void OnClickMover(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            _cameraDelta = Vector2.zero;
        }
        else if (context.performed)
        {
            _cameraDelta = context.ReadValue<Vector2>();
            _cameraMover.DragMove(_cameraDelta);
        }
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            _tapPoint = Vector2.zero;
        }
        else if (context.performed)
        {
            context.ReadValue<Vector2>();
            _tapPoint = context.ReadValue<Vector2>();
            _soung.PlayClickSoung();

            if (_tapPoint != Vector2.zero)
            {
                Ray castPoint = Camera.main.ScreenPointToRay(_tapPoint);
                RaycastHit hit;

                if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
                {
                    if (hit.collider.TryGetComponent(out WaterZone _))
                    {
                        _soung.PlayWaterSplash();
                    }
                    else
                    {
                        _soung.PlayClickSoung();
                    }

                    if (hit.collider.TryGetComponent(out Car car))
                    {
                        car.Move();
                    }

                    if (hit.collider.TryGetComponent(out TileHelper tile))
                    {
                        Clicked?.Invoke(tile.cordX, tile.cordY);
                    }

                    context = new InputAction.CallbackContext();
                    _input.Player.Click.performed -= OnClick;
                    StartCoroutine(Cooldown());
                }
            }
        }
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(0.3f);

        _input.Player.Click.performed += OnClick;
    }
}
