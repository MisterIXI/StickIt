using UnityEngine;

public class TimeRule : BaseRule
{
    public override string RuleDescription => "rush";
    [field: SerializeField] public float TimeLimit { get; private set; } = 30f;
    private void Start()
    {
        RuleManager.Instance.OnRuleActiveChange += CheckForCompletions;
    }
    private void FixedUpdate()
    {
        if (RuleActive)
        {
            RuleProgress(Time.fixedDeltaTime / TimeLimit, false);
        }
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

    private void OnDestroy()
    {
        if (RuleManager.Instance != null)
            RuleManager.Instance.OnRuleActiveChange -= CheckForCompletions;
    }

}