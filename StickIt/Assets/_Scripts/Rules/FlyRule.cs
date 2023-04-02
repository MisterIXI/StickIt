using UnityEngine;

public class FlyRule : BaseRule
{
    public override string RuleDescription => "fly";

    [field: SerializeField] private float _targetTime { get; set; } = 5f;

    private float _timeLeftGround = 0f;
    private bool _isGrounded = true;

    private void Start()
    {
        GroundedCheck.OnGroundedStateChange += OnGroundedStateChange;
    }

    private void FixedUpdate()
    {
        if (RuleActive)
        {
            if (!_isGrounded)
            {
                RuleProgress(Time.fixedDeltaTime / _targetTime, true);
            }
        }
    }

    private void OnGroundedStateChange(bool isGrounded)
    {
        if (RuleActive)
        {
            _isGrounded = isGrounded;
            if (!isGrounded)
            {
                _timeLeftGround = Time.time;
            }
            else
            {
                SetRuleProgress(0f, true);
            }
        }
    }

    private void OnDestroy()
    {
        GroundedCheck.OnGroundedStateChange -= OnGroundedStateChange;
    }
}