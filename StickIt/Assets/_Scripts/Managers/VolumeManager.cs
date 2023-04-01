using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class VolumeManager : MonoBehaviour
{
    public static VolumeManager Instance { get; private set; }
    private Volume _volume;
    private Vignette _vignette;
    private DepthOfField _depthOfField;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        transform.SetParent(null);
        DontDestroyOnLoad(gameObject);
        _volume = GetComponent<Volume>();
        _volume.profile.TryGet(out _vignette);
        _volume.profile.TryGet(out _depthOfField);
        GameManager.OnGameStateChanged += OnGameStateChange;
    }


    private void OnGameStateChange(GameState oldState, GameState newState)
    {
        if (newState == GameState.Paused)
        {
            _vignette.active = true;
            _depthOfField.active = true;
        }
        else
        {
            _vignette.active = false;
            _depthOfField.active = false;
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }
}