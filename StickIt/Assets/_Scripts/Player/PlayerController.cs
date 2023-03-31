using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(GroundedCheck))]
public class PlayerController : MonoBehaviour
{
    private PlayerSettings _playerSettings;
    private Vector2 _moveInput;
    private Vector2 _moveVelocity;
    private Rigidbody2D _rb;
    private GroundedCheck _groundedCheck;

    public static event Action OnJump;
    private void Awake()
    {
        _playerSettings = Settingsmanager.Instance.PlayerSettings;
        _groundedCheck = GetComponent<GroundedCheck>();
        _rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        InputManager.OnMove += OnMoveInput;
        InputManager.OnJump += OnJumpInput;
    }

    private void FixedUpdate()
    {
        MoveUpdate();
    }

    private void MoveUpdate()
    {
        float xVelocity = Mathf.MoveTowards(_rb.velocity.x, _moveInput.x * _playerSettings.MovementSpeed, _playerSettings.Acceleration * Time.fixedDeltaTime);
        _rb.velocity = new Vector2(xVelocity, _rb.velocity.y);
    }
    private void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _moveInput = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            _moveInput = Vector2.zero;
        }
    }

    private void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started && _groundedCheck.IsGrounded)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _playerSettings.JumpVelocity);
            OnJump?.Invoke();
        }
        else if (context.canceled && _groundedCheck.IsJumping)
        {
            Vector2 velocity = _rb.velocity;
            velocity.y = Mathf.Clamp(velocity.y, float.MinValue, _playerSettings.JumpReleaseCap);
            _rb.velocity = velocity;
        }
    }

    private void OnDestroy()
    {
        InputManager.OnMove -= OnMoveInput;
        InputManager.OnJump -= OnJumpInput;
    }
    private void OnDrawGizmos()
    {
        if (_playerSettings != null)
        {
            if (_playerSettings.MovementGizmos)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawRay(transform.position, Vector3.right * _rb.velocity.x);
                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere(transform.position + Vector3.right * _moveInput.x * _playerSettings.MovementSpeed, 0.1f);
            }
        }
    }
}
