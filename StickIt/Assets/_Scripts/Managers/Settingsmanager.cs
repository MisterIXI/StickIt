using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    private static SettingsManager _instance;
    public static SettingsManager Instance
    {
        get
        {
            return _instance ?? FindObjectOfType<SettingsManager>();
        }
    }
    [field: SerializeField] public PlayerSettings PlayerSettings { get; private set; }
    [field: SerializeField] public TapeSettings TapeSettings { get; private set; }
    [field: SerializeField] public LevelSettings LevelSettings { get; private set; }
    [field: SerializeField] public SoundFiles SoundFiles { get; private set; }
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        if (_instance == this)
            _instance = null;
    }
}