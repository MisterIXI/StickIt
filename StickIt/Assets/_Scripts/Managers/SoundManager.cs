using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    private float _masterVolume => SettingsMenu.MasterVolume;
    private float _musicVolume => SettingsMenu.MusicVolume;
    private float _sfxVolume => SettingsMenu.SFXVolume;
    [field: SerializeField] private AudioSource _musicSource;
    [field: SerializeField] private AudioSource _sfxSource;
    private SoundFiles _soundFiles;
    private float _timeOfLastSuccessSound;
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
        PlayerController.OnJump += OnJump;
        PlayerController.OnPlayerDeath += OnDeath;
        TapeController.OnTapeFinished += OnTapeFinish;
        _soundFiles = SettingsManager.Instance.SoundFiles;
    }


    private void OnJump()
    {
        // get random jump sound
        var jumpSound = _soundFiles.JumpSounds[Random.Range(0, _soundFiles.JumpSounds.Length)];
        _sfxSource.PlayOneShot(jumpSound, _sfxVolume * _masterVolume);
    }

    private void OnDeath()
    {
        // get random death sound
        var deathSound = _soundFiles.DeathSounds[Random.Range(0, _soundFiles.DeathSounds.Length)];
        _sfxSource.PlayOneShot(deathSound, _sfxVolume * _masterVolume);
    }

    private void OnTapeFinish()
    {
        // get random tape sound
        var tapeSound = _soundFiles.TapeSounds[Random.Range(0, _soundFiles.TapeSounds.Length)];
        _sfxSource.PlayOneShot(tapeSound, _sfxVolume * _masterVolume);
    }

    public static void PlayButtonClick()
    {
        // get random click sound
        var clickSound = Instance._soundFiles.ClickSounds[Random.Range(0, Instance._soundFiles.ClickSounds.Length)];
        Instance._sfxSource.PlayOneShot(clickSound, Instance._sfxVolume * Instance._masterVolume);
    }

    public static void PlaySuccessSound()
    {
        if (Time.time - Instance._timeOfLastSuccessSound < 0.3f)
            return;
        // get random success sound
        var successSound = Instance._soundFiles.SuccessSounds[Random.Range(0, Instance._soundFiles.SuccessSounds.Length)];
        Instance._sfxSource.PlayOneShot(successSound, Instance._sfxVolume * Instance._masterVolume);
        Instance._timeOfLastSuccessSound = Time.time;
    }

    public static void PlayFailSound()
    {
        // get random fail sound
        var failSound = Instance._soundFiles.FailSounds[Random.Range(0, Instance._soundFiles.FailSounds.Length)];
        Instance._sfxSource.PlayOneShot(failSound, Instance._sfxVolume * Instance._masterVolume);
    }

    public static void PlayCutSound()
    {
        // get random cut sound
        var cutSound = Instance._soundFiles.CutSounds[Random.Range(0, Instance._soundFiles.CutSounds.Length)];
        Instance._sfxSource.PlayOneShot(cutSound, Instance._sfxVolume * Instance._masterVolume);
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }
}