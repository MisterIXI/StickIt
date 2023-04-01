using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MenuBase
{
    [field: SerializeField] public Button BackButton { get; private set; }
    [field: SerializeField] public Slider MasterVolumeSlider { get; private set; }
    [field: SerializeField] public Slider MusicVolumeSlider { get; private set; }
    [field: SerializeField] public Slider SFXVolumeSlider { get; private set; }
    public static float MasterVolume { get; private set; }
    public static float MusicVolume { get; private set; }
    public static float SFXVolume { get; private set; }

    public override void Init()
    {
        BackButton.onClick.AddListener(ToMainMenu);
        MasterVolume = PlayerPrefs.GetFloat("MasterVolume", 0.3f);
        MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.3f);
        SFXVolume = PlayerPrefs.GetFloat("SFXVolume", 0.3f);
        MasterVolumeSlider.value = MasterVolume;
        MusicVolumeSlider.value = MusicVolume;
        SFXVolumeSlider.value = SFXVolume;
        MasterVolumeSlider.onValueChanged.AddListener(OnMasterVolumeChanged);
        MusicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        SFXVolumeSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
    }

    private void OnMasterVolumeChanged(float value)
    {
        MasterVolume = value;
    }

    private void OnMusicVolumeChanged(float value)
    {
        MusicVolume = value;
    }

    private void OnSFXVolumeChanged(float value)
    {
        SFXVolume = value;
    }

    // public override void SelectFirst()
    // {
    //     BackButton.Select();
    // }
}