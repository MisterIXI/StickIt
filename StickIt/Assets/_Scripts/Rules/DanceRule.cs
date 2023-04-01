using UnityEngine;
using UnityEngine.InputSystem;
public class DanceRule : BaseRule
{
    public override string RuleDescription => "dance";
    [field: SerializeField] public float ProgressPerKey { get; private set; } = 0.05f;
    [field: SerializeField] public float ProgressLostPerSecond { get; private set; } = 1f;

    private void Start()
    {
        Subscribe();
    }
    private void FixedUpdate()
    {
        if (RuleActive)
        {
            RuleProgress(-ProgressLostPerSecond * Time.fixedDeltaTime, true);
        }
    }

    private void AddProgress()
    {
        if (RuleActive)
        {
            RuleProgress(ProgressPerKey, true);
        }
        else
        {
            UnSubscribe();
        }
    }

    private void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            AddProgress();
        }
    }
    private void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            AddProgress();
        }
    }

    private void Subscribe()
    {
        InputManager.OnMove += OnMoveInput;
        InputManager.OnJump += OnJumpInput;
    }
    private void UnSubscribe()
    {
        InputManager.OnMove -= OnMoveInput;
        InputManager.OnJump -= OnJumpInput;
    }

    private void OnDestroy()
    {
        UnSubscribe();
    }

}