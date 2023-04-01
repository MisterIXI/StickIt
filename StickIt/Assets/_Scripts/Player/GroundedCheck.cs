using UnityEngine;
using UnityEngine.InputSystem;

public class GroundedCheck : MonoBehaviour
{
    private PlayerSettings _playerSettings;
    public bool IsGrounded { get; private set; } = false;
    private bool _drawGizmos => _playerSettings.GroundedCheckGizmos;
    private float _verticalOffset => _playerSettings.GroundedCheckVerticalOffset;
    private float _checkWidth => _playerSettings.GroundedCheckWidth;
    private float _checkHeight => _playerSettings.GroundedCheckHeight;

    public bool IsJumping { get; private set; } = false;
    private float _lastGroundedTime = 0f;
    private bool _isGrounded = false;
    private Rigidbody2D _rigidbody2D;
    private void Awake()
    {
        _playerSettings = SettingsManager.Instance.PlayerSettings;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        PlayerController.OnJump += OnJump;
    }
    private void FixedUpdate()
    {
        if (IsJumping)
        {
            if (_rigidbody2D.velocity.y <= 0)
                IsJumping = false;
        }
        else
        {
            UpdateGroundedState();
        }
    }

    private void UpdateGroundedState()
    {
        bool newGrounded = Physics2D.OverlapBox(transform.position + Vector3.down * _verticalOffset, new Vector2(_checkWidth, _checkHeight), 0, _playerSettings.GroundedCheckLayerMask);
        if (newGrounded)
            IsGrounded = true;
        else
        {
            if (IsGrounded && Time.time - _lastGroundedTime > _playerSettings.CayoteeTime)
            {
                IsGrounded = false;
            }
        }
        if (_isGrounded && !newGrounded)
            _lastGroundedTime = Time.time;
        _isGrounded = newGrounded;
    }

    private void OnJump()
    {
        IsJumping = true;
        _isGrounded = false;
        IsGrounded = false;
    }

    private void OnDestroy()
    {
        PlayerController.OnJump -= OnJump;
    }
    private void OnDrawGizmos()
    {
        if (_playerSettings is not null && _drawGizmos)
        {
            Gizmos.color = IsGrounded ? Color.green : Color.red;
            Gizmos.DrawWireCube(transform.position + Vector3.down * _verticalOffset, new Vector3(_checkWidth, _checkHeight, 0));
        }
    }
}