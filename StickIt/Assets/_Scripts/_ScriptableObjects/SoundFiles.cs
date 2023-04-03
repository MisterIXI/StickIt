using UnityEngine;

[CreateAssetMenu(fileName = "SoundFiles", menuName = "StickIt/SoundFiles", order = 0)]
public class SoundFiles : ScriptableObject
{
    [field: SerializeField] public AudioClip[] TapeSounds { get; private set; }
    [field: SerializeField] public AudioClip[] ClickSounds { get; private set; }
    [field: SerializeField] public AudioClip[] JumpSounds { get; private set; }
    [field: SerializeField] public AudioClip[] DeathSounds { get; private set; }
    [field: SerializeField] public AudioClip[] SuccessSounds { get; private set; }
    [field: SerializeField] public AudioClip[] FailSounds { get; private set; }
    [field: SerializeField] public AudioClip[] CutSounds { get; private set; }
}