using UnityEngine;

[CreateAssetMenu(fileName = "TapeSettings", menuName = "StickIt/TapeSettings", order = 0)]
public class TapeSettings : ScriptableObject
{
    [field: Header("TapePool")]
    [field: SerializeField] public int TapePoolInitSize { get; private set; } = 10;
    [field: Header("Tape")]
    [field: SerializeField] public bool TapeGizmos { get; private set; } = false;
    [field: SerializeField][field: Range(0f, 3f)] public float TapeMinLength { get; private set; } = 0.5f;
}