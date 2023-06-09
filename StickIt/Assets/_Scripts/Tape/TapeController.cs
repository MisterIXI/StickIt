using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class TapeController : MonoBehaviour
{
    public static event Action OnTapeFinished;
    private Vector2 _mouseInput;
    private Vector2 _mousePos;
    private Vector2 _tapeStartPos;
    private TapeUpdater _currentTape;
    [field: SerializeField] private TapeCutter _tapeCutter;
    private TapeSettings _tapeSettings;
    private bool _tapeCutterActive = false;
    private void Awake()
    {
        if (_tapeCutter == null)
        {
            Debug.LogError("TapeCutter is null, self destructing...");
            Destroy(gameObject);
            return;
        }
        InputManager.OnLMB += OnLMBInput;
        InputManager.OnRMB += OnRMBInput;
        InputManager.OnMouseMove += OnMousePosInput;
        _tapeSettings = SettingsManager.Instance.TapeSettings;
    }
    private void Update()
    {
        _mousePos = Camera.main.ScreenToWorldPoint(_mouseInput);
        if (_currentTape != null)
        {
            UpdateTape();
        }
        if (_tapeCutterActive)
        {
            _tapeCutter.CutToPosition(_mousePos);
        }
    }

    private void UpdateTape()
    {
        _currentTape.UpdateTape(_tapeStartPos, _mousePos);
    }

    private void StartTape()
    {
        _currentTape = TapePool.GetTape();
        _tapeStartPos = _mousePos;
    }

    private void CancelTape()
    {
        TapePool.ReturnTape(_currentTape);
        _currentTape.CleanUp();
        _currentTape = null;
    }

    private void FinishTape()
    {
        UpdateTape();
        if (_currentTape.TapeLength > _tapeSettings.TapeMinLength)
        {
            _currentTape.StickIt();
            _currentTape = null;
            OnTapeFinished?.Invoke();
        }
        else
        {
            CancelTape();
        }
    }

    private void OnLMBInput(InputAction.CallbackContext context)
    {
        // Debug.Log(GameManager.GameState);
        if (GameManager.GameState != GameState.Playing) return;
        if (context.started)
        {
            StartTape();
        }
        else if (context.canceled && _currentTape != null)
        {
            FinishTape();
        }
    }

    private void OnRMBInput(InputAction.CallbackContext context)
    {
        if (GameManager.GameState != GameState.Playing) return;
        if (context.started)
        {
            if (_currentTape != null)
            {
                CancelTape();
            }
            _tapeCutter.ResetPosition(_mousePos);
            _tapeCutter.gameObject.SetActive(true);
            _tapeCutter.GetComponent<TrailRenderer>().Clear();
            _tapeCutterActive = true;
        }
        else if (context.canceled)
        {
            _tapeCutter.gameObject.SetActive(false);
            _tapeCutterActive = false;
        }
    }

    private void OnMousePosInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // mouse position in world space
            _mouseInput = context.ReadValue<Vector2>();
            // _mousePos = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
        }
    }

    private void OnDestroy()
    {
        InputManager.OnLMB -= OnLMBInput;
        InputManager.OnRMB -= OnRMBInput;
        InputManager.OnMouseMove -= OnMousePosInput;
    }
}