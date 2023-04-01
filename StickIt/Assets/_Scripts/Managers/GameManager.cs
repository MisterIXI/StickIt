using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static GameState GameState { get; private set; }
    private LevelSettings _levelSettings;
    public static event Action<GameState, GameState> OnGameStateChanged;
    public static event Action OnSceneLoad;
    [field: SerializeField] private bool _startInLevel;
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
        _levelSettings = SettingsManager.Instance.LevelSettings;
        OnGameStateChanged += OnGameStateChange;
        SceneManager.activeSceneChanged += OnSceneChanged;
        ChangeGameState(GameState.Playing);
    }

    private void Start()
    {
        if (_startInLevel)
        {
            ChangeGameState(GameState.Playing);
            MenuManager.SwitchMenu(MenuState.HUD);
        }
        else
        {
            MenuManager.SwitchMenu(MenuState.MainMenu);
        }
    }

    private void OnSceneChanged(Scene oldScene, Scene newScene)
    {
        Debug.Log($"Scene changed from {oldScene.name} to {newScene.name}");
        OnSceneLoad?.Invoke();
    }
    public static void LoadLevel(string levelName)
    {
        // check if level is in levelcollection
        if (!Instance._levelSettings.LevelCollection.Any(l => l.name == levelName))
        {
            Debug.LogError($"Level {levelName} not found in LevelCollection");
            return;
        }
        SceneManager.LoadScene(levelName);
        ChangeGameState(GameState.Playing);
    }

    public static void RetryLevel()
    {
        LoadLevel(SceneManager.GetActiveScene().name);
    }
    public static void ChangeGameState(GameState newState)
    {
        GameState oldState = GameState;
        GameState = newState;
        if (oldState != newState)
            OnGameStateChanged?.Invoke(oldState, newState);
    }
    private void OnGameStateChange(GameState oldState, GameState newState)
    {
        if (newState == GameState.Paused)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
            OnGameStateChanged -= OnGameStateChange;
        }
    }
}

public enum GameState
{
    Menu,
    Playing,
    Paused
}