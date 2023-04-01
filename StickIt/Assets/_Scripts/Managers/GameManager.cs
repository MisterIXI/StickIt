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
        _levelSettings = Settingsmanager.Instance.LevelSettings;
        OnGameStateChanged += OnGameStateChange;
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