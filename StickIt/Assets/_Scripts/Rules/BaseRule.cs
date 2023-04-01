using System;
using UnityEngine;
public abstract class BaseRule : MonoBehaviour
{
    public abstract string RuleDescription { get; }
    public event Action<BaseRule> OnRuleCompleted;
    public event Action<BaseRule> OnRuleFailed;
    public event Action<BaseRule, float, bool> OnRuleProgress;
    public float CurrentProgress { get; protected set; } = 0f;
    public bool RuleActive { get; protected set; } = true;
    protected void RuleCompleted()
    {
        RuleActive = false;
        OnRuleCompleted?.Invoke(this);
    }
    protected void RuleFailed()
    {
        RuleActive = false;
        OnRuleFailed?.Invoke(this);
    }
    protected void RuleProgress(float change, bool isPositiveProgress)
    {
        CurrentProgress = Mathf.Clamp(CurrentProgress + change, 0f, 1f);
        if (CurrentProgress >= 1f)
        {
            if (isPositiveProgress)
                RuleCompleted();
            else
                RuleFailed();
        }
        else
        {
            OnRuleProgress?.Invoke(this, CurrentProgress, isPositiveProgress);
        }
    }
}