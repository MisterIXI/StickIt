using UnityEngine;
using UnityEngine.InputSystem;
public class StandStillRule : BaseRule
{
    public override string RuleDescription => "stand still";
    private void Start()
    {
        Subscribe();
        RuleManager.Instance.OnRuleActiveChange += CheckForCompletions;
    }

    private void CheckForCompletions()
    {
        if (RuleActive)
        {
            if (RuleManager.Instance.CompletedRules == RuleManager.Instance.Rules.Length - 1)
            {
                RuleCompleted();
            }
        }
    }
    private void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            RuleFailed();
        }
    }
    private void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            RuleFailed();
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
        if (RuleManager.Instance != null)
            RuleManager.Instance.OnRuleActiveChange -= CheckForCompletions;
    }
}