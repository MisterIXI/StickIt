using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    private PlayerInput _playerInput;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        transform.SetParent(null);
        DontDestroyOnLoad(gameObject);
        _playerInput = GetComponent<PlayerInput>();
        SubscribeToInput();
    }
    public static event Action<CallbackContext> OnMove;
    public static event Action<CallbackContext> OnMouseMove;
    public static event Action<CallbackContext> OnLMB;
    public static event Action<CallbackContext> OnRMB;
    public static event Action<CallbackContext> OnJump;
    public static event Action<CallbackContext> OnPause;
    private void OnMoveInput(CallbackContext context)
    {
        OnMove?.Invoke(context);
    }

    private void OnMouseMoveInput(CallbackContext context)
    {
        OnMouseMove?.Invoke(context);
    }

    private void OnLMBInput(CallbackContext context)
    {
        OnLMB?.Invoke(context);
    }

    private void OnRMBInput(CallbackContext context)
    {
        OnRMB?.Invoke(context);
    }

    private void OnJumpInput(CallbackContext context)
    {
        OnJump?.Invoke(context);
    }

    private void OnPauseInput(CallbackContext context)
    {
        OnPause?.Invoke(context);
    }

    private void SubscribeToInput()
    {
        _playerInput.actions["Move"].started += OnMoveInput;
        _playerInput.actions["Move"].performed += OnMoveInput;
        _playerInput.actions["Move"].canceled += OnMoveInput;

        _playerInput.actions["MouseMove"].started += OnMouseMoveInput;
        _playerInput.actions["MouseMove"].performed += OnMouseMoveInput;
        _playerInput.actions["MouseMove"].canceled += OnMouseMoveInput;

        _playerInput.actions["LMB"].started += OnLMBInput;
        _playerInput.actions["LMB"].performed += OnLMBInput;
        _playerInput.actions["LMB"].canceled += OnLMBInput;

        _playerInput.actions["RMB"].started += OnRMBInput;
        _playerInput.actions["RMB"].performed += OnRMBInput;
        _playerInput.actions["RMB"].canceled += OnRMBInput;

        _playerInput.actions["Jump"].started += OnJumpInput;
        _playerInput.actions["Jump"].performed += OnJumpInput;
        _playerInput.actions["Jump"].canceled += OnJumpInput;

        _playerInput.actions["Pause"].started += OnPauseInput;
        _playerInput.actions["Pause"].performed += OnPauseInput;
        _playerInput.actions["Pause"].canceled += OnPauseInput;
    }

    private void UnSubscribeToInput()
    {
        _playerInput.actions["Move"].started -= OnMoveInput;
        _playerInput.actions["Move"].performed -= OnMoveInput;
        _playerInput.actions["Move"].canceled -= OnMoveInput;

        _playerInput.actions["MouseMove"].started -= OnMouseMoveInput;
        _playerInput.actions["MouseMove"].performed -= OnMouseMoveInput;
        _playerInput.actions["MouseMove"].canceled -= OnMouseMoveInput;

        _playerInput.actions["LMB"].started -= OnLMBInput;
        _playerInput.actions["LMB"].performed -= OnLMBInput;
        _playerInput.actions["LMB"].canceled -= OnLMBInput;

        _playerInput.actions["RMB"].started -= OnRMBInput;
        _playerInput.actions["RMB"].performed -= OnRMBInput;
        _playerInput.actions["RMB"].canceled -= OnRMBInput;

        _playerInput.actions["Jump"].started -= OnJumpInput;
        _playerInput.actions["Jump"].performed -= OnJumpInput;
        _playerInput.actions["Jump"].canceled -= OnJumpInput;

        _playerInput.actions["Pause"].started -= OnPauseInput;
        _playerInput.actions["Pause"].performed -= OnPauseInput;
        _playerInput.actions["Pause"].canceled -= OnPauseInput;
    }


    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
        UnSubscribeToInput();
    }
}