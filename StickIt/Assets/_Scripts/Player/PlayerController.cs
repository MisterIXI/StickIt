using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(GroundedCheck))]
public class PlayerController : MonoBehaviour
{
    public static event Action OnPlayerDeath;
    public static event Action OnJump;
    private PlayerSettings _playerSettings;
    private Vector2 _moveInput;
    private Vector2 _moveVelocity;
    private Rigidbody2D _rb;
    private GroundedCheck _groundedCheck;
    private bool _isBreaking = true;
    public static PlayerController Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            Debug.LogWarning("PlayerController already exists");
            return;
        }
        Instance = this;
        _playerSettings = SettingsManager.Instance.PlayerSettings;
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
        if (_moveInput != Vector2.zero || _isBreaking)
        {
            float xVelocity = Mathf.MoveTowards(_rb.velocity.x, _moveInput.x * _playerSettings.MovementSpeed, _playerSettings.Acceleration * Time.fixedDeltaTime);
            _rb.velocity = new Vector2(xVelocity, _rb.velocity.y);
        }
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
            if (velocity.y < _playerSettings.JumpVelocity * 2f)
                velocity.y = Mathf.Clamp(velocity.y, float.MinValue, _playerSettings.JumpReleaseCap);
            _rb.velocity = velocity;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("KillZone"))
        {
            OnPlayerDeath?.Invoke();
            _rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else if (other.CompareTag("NoBreakZone"))
        {
            _isBreaking = false;
            Debug.Log($"Player breaking: {_isBreaking}");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("NoBreakZone"))
        {
            _isBreaking = true;
            Debug.Log($"Player breaking: {_isBreaking}");
        }
    }
    private void OnDestroy()
    {
        InputManager.OnMove -= OnMoveInput;
        InputManager.OnJump -= OnJumpInput;
        if (Instance == this)
            Instance = null;
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
