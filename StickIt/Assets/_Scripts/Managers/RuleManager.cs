using System;
using UnityEngine;
public class RuleManager : MonoBehaviour
{
    public static RuleManager Instance { get; private set; }
    [field: SerializeField] public BaseRule[] Rules { get; private set; }
    public event Action OnAllRulesCompleted;
    public event Action<RuleChangeType, BaseRule, float, bool> OnRuleChange;
    public event Action OnRuleActiveChange;
    public float CompletedRules { get; private set; } = 0f;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        foreach (var rule in Rules)
        {
            rule.OnRuleCompleted += OnRuleCompletion;
            rule.OnRuleFailed += OnRuleFailure;
            rule.OnRuleProgress += OnRuleProgress;
        }
    }

    private void OnRuleCompletion(BaseRule rule)
    {
        CompletedRules++;
        if (CompletedRules == Rules.Length)
        {
            OnAllRulesCompleted?.Invoke();
            LevelManager.MarkCurrentLevelAsComplete();
            Debug.Log("All rules completed");
        }
        OnRuleChange?.Invoke(RuleChangeType.Completion, rule, 1f, true);
        OnRuleActiveChange?.Invoke();
    }

    private void OnRuleFailure(BaseRule rule)
    {
        OnRuleActiveChange?.Invoke();
        OnRuleChange?.Invoke(RuleChangeType.Fail, rule, 0f, false);
    }

    private void OnRuleProgress(BaseRule rule, float progress, bool isPositiveProgress)
    {
        OnRuleChange?.Invoke(RuleChangeType.Progress, rule, progress, isPositiveProgress);
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }
}
public enum RuleChangeType
{
    Completion,
    Fail,
    Progress
}