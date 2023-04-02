using System;
using UnityEngine;
public class RuleManager : MonoBehaviour
{
    public static RuleManager Instance { get; private set; }
    [field: SerializeField] public BaseRule[] Rules { get; private set; }
    public event Action OnAllRulesCompleted;
    public event Action<RuleChangeType, BaseRule, float, bool> OnRuleChange;
    private float _completedRules;
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
        OnRuleChange?.Invoke(RuleChangeType.Completion, rule, 1f, true);
        _completedRules++;
        if (_completedRules == Rules.Length)
        {
            OnAllRulesCompleted?.Invoke();
            LevelManager.MarkCurrentLevelAsComplete();
            Debug.Log("All rules completed");
        }
    }

    private void OnRuleFailure(BaseRule rule)
    {
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