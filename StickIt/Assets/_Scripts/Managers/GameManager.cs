using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
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
        InputManager.OnRestart += OnRestartInput;
        InputManager.OnBackToMenu += OnBackToMenuInput;
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
        if (!Instance._levelSettings.LevelCollection.Any(l => l == levelName))
        {
            Debug.LogError($"Level {levelName} not found in LevelCollection");
            return;
        }
        SceneManager.LoadScene(levelName);
        ChangeGameState(GameState.Playing);
    }

    public static void LoadMainMenu()
    {
        SceneManager.LoadScene("MainScene");
        ChangeGameState(GameState.Menu);
        MenuManager.SwitchMenu(MenuState.MainMenu);
    }

    public static void RetryLevel()
    {
        // SceneManager.LoadScene("MainScene");
        LoadLevel(SceneManager.GetActiveScene().name);
        GameManager.ChangeGameState(GameState.Playing);
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
        Debug.Log($"Game state changed from {oldState} to {newState}");
        if (newState == GameState.Paused)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
        if (newState == GameState.Playing)
            MenuManager.SwitchMenu(MenuState.HUD);
    }
    private void OnRestartInput(InputAction.CallbackContext context)
    {
        if (context.performed && GameState == GameState.Playing)
            RetryLevel();
    }

    private void OnBackToMenuInput(InputAction.CallbackContext context)
    {
        if (context.performed && GameState == GameState.Playing)
            LoadMainMenu();
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
            OnGameStateChanged -= OnGameStateChange;
            SceneManager.activeSceneChanged -= OnSceneChanged;
            InputManager.OnRestart -= OnRestartInput;
            InputManager.OnBackToMenu -= OnBackToMenuInput;
        }
    }
}

public enum GameState
{
    Menu,
    Playing,
    Paused
}