using UnityEngine;

public class FlagGoal : BaseRule
{
    public override string RuleDescription => "touch the flag";

    private float _currentProgress = 0f;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (RuleActive)
            {
                _currentProgress += 0.1f;
                RuleProgress(_currentProgress, true);
                // RuleFailed();
                Debug.Log("Rule completed");
            }
        }
    }

    private void OnDestroy()
    {
        // only called if object is destroyed during runtime and not scene cleanup
        if (gameObject.scene.isLoaded)
            RuleFailed();
    }
}