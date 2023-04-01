using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    private float _masterVolume => SettingsMenu.MasterVolume;
    private float _musicVolume => SettingsMenu.MusicVolume;
    private float _sfxVolume => SettingsMenu.SFXVolume;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        transform.parent = null;
        DontDestroyOnLoad(gameObject);
    }


    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }
}