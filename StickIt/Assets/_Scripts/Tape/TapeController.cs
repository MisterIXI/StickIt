using UnityEngine;
using UnityEngine.InputSystem;
public class TapeController : MonoBehaviour
{
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
        _tapeSettings = Settingsmanager.Instance.TapeSettings;
    }
    private void Update()
    {
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
        _currentTape = null;
    }

    private void FinishTape()
    {
        UpdateTape();
        _currentTape = null;
    }

    private void OnLMBInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            StartTape();
        }
        else if (context.canceled)
        {
            FinishTape();
        }
    }

    private void OnRMBInput(InputAction.CallbackContext context)
    {
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
            _mousePos = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
        }
    }

    private void OnDestroy()
    {
        InputManager.OnLMB -= OnLMBInput;
        InputManager.OnRMB -= OnRMBInput;
        InputManager.OnMouseMove -= OnMousePosInput;
    }
}