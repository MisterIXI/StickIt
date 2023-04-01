using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "StickIt/PlayerSettings", order = 0)]
public class PlayerSettings : ScriptableObject
{
    [field: Header("Movement")]
    [field: SerializeField] public bool MovementGizmos { get; private set; } = false;
    [field: SerializeField][field: Range(0.01f, 50f)] public float MovementSpeed { get; private set; } = 1f;
    [field: SerializeField][field: Range(0.01f, 100f)] public float Acceleration { get; private set; } = 5f;
    [field: Header("Jumping")]
    [field: SerializeField] public float CayoteeTime { get; private set; }
    [field: SerializeField] public float JumpVelocity { get; private set; }
    [field: SerializeField] public float JumpReleaseCap { get; private set; }

    [field: Header("GroundedCheck")]
    [field: SerializeField] public LayerMask GroundedCheckLayerMask { get; private set; }
    [field: SerializeField] public bool GroundedCheckGizmos { get; private set; } = false;
    [field: SerializeField] public float GroundedCheckVerticalOffset { get; private set; } = 0.58f;
    [field: SerializeField] public float GroundedCheckWidth { get; private set; } = 1f;
    [field: SerializeField] public float GroundedCheckHeight { get; private set; } = 0.18f;

    [field: Header("CameraSettings")]
    [field: SerializeField] public bool CameraGizmos { get; private set; } = false;
    [field: SerializeField][field: Range(0f, 1f)] public float CameraFollowTime { get; private set; } = 0.1f;
    [field: SerializeField] public float CameraMaxSpeed { get; private set; } = 10f;
    [field: SerializeField] public float CameraIdleDistance { get; private set; } = 20f;


}