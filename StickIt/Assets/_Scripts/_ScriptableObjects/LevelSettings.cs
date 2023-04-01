using UnityEngine;
using UnityEngine.SceneManagement;
[CreateAssetMenu(fileName = "LevelSettings", menuName = "StickIt/LevelSettings", order = 0)]
public class LevelSettings : ScriptableObject
{
    [field: Header("Levelcollection")]
    [field: SerializeField] public float LevelCount { get; private set; }
    [field: SerializeField] public string[] LevelCollection;
}