using UnityEngine;
using UnityEngine.InputSystem;
public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; private set; }
    [field: SerializeField] public MenuBase MainMenu { get; private set; }
    [field: SerializeField] public MenuBase LevelSelect { get; private set; }
    [field: SerializeField] public MenuBase HUD { get; private set; }
    [field: SerializeField] public MenuBase Pause { get; private set; }
    private MenuBase _currentMenu;
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
    private void OnPauseButtonInput(InputAction.CallbackContext context)
    {
        if (GameManager.GameState != GameState.Paused)
        {
            GameManager.ChangeGameState(GameState.Paused);
            SwitchMenu(MenuState.Pause);
        }
        else if (_currentMenu == Pause)
        {
            GameManager.ChangeGameState(GameState.Playing);
            SwitchMenu(MenuState.HUD);
        }
    }
    public static void SwitchMenu(MenuState menuState)
    {
        Instance._currentMenu?.gameObject.SetActive(false);
        switch (menuState)
        {
            case MenuState.MainMenu:
                Instance._currentMenu = Instance.MainMenu;
                break;
            case MenuState.LevelSelect:
                Instance._currentMenu = Instance.LevelSelect;
                break;
            case MenuState.HUD:
                Instance._currentMenu = Instance.HUD;
                break;
            case MenuState.Pause:
                Instance._currentMenu = Instance.Pause;
                break;

        }
        Instance._currentMenu.SelectFirst();
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }
}

public enum MenuState
{
    MainMenu,
    LevelSelect,
    Settings,
    Controls,
    Credits,
    HUD,
    Pause
}