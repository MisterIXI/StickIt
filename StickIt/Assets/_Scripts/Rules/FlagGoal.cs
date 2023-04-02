using UnityEngine;

public class FlagGoal : BaseRule
{
    public override string RuleDescription => "touch the flag";

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (RuleActive && other.gameObject.CompareTag("Player"))
        {
            RuleCompleted();
        }
    }

    private void OnDestroy()
    {
        // only called if object is destroyed during runtime and not scene cleanup
        if (gameObject.scene.isLoaded)
            RuleFailed();
    }
}