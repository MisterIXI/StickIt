using UnityEngine;

public class Settingsmanager : MonoBehaviour
{
    private static Settingsmanager _instance;
    public static Settingsmanager Instance
    {
        get
        {
            return _instance ?? FindObjectOfType<Settingsmanager>();
        }
    }
    [field: SerializeField] public PlayerSettings PlayerSettings { get; private set; }
    [field: SerializeField] public TapeSettings TapeSettings { get; private set; }
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