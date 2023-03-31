using UnityEngine;
using UnityEngine.InputSystem;
public class TapeController : MonoBehaviour
{
    private Vector2 _mousePos;
    private SpriteRenderer _currentTape;
    [field: SerializeField] private GameObject _tapeCutter;
    private void Awake()
    {
        if (_tapeCutter == null)
        {
            Debug.LogError("TapeCutter is null, self destructing...");
            Destroy(gameObject);
            return;
        }
        InputManager.OnLMB += OnLMBInput;
        InputManager.OnMouseMove += OnMousePosInput;
    }

    private void StartTape()
    {

    }

    private void CancelTape()
    {

    }

    private void EndTape()
    {

    }

    private void OnLMBInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            StartTape();
        }
        else if (context.canceled)
        {
            EndTape();
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
            _tapeCutter.SetActive(true);
        }
        else if (context.canceled)
        {
            _tapeCutter.SetActive(false);
        }
    }

    private void OnMousePosInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _mousePos = context.ReadValue<Vector2>();
        }
    }

    private void OnDestroy()
    {
        InputManager.OnLMB -= OnLMBInput;
        InputManager.OnMouseMove -= OnMousePosInput;
    }
}