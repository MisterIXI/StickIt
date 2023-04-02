using UnityEngine;

public class DeathRule : BaseRule
{
    public override string RuleDescription => "die";

    private void Awake()
    {
        PlayerController.OnPlayerDeath += OnPlayerDeath;
    }

    private void OnPlayerDeath()
    {
        RuleCompleted();
    }

    private void OnDestroy()
    {
        PlayerController.OnPlayerDeath -= OnPlayerDeath;
    }
}