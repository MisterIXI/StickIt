using UnityEngine;

[CreateAssetMenu(fileName = "TapeSettings", menuName = "StickIt/TapeSettings", order = 0)]
public class TapeSettings : ScriptableObject
{
    [field: Header("TapePool")]
    [field: SerializeField] public int TapePoolInitSize { get; private set; } = 10;
}