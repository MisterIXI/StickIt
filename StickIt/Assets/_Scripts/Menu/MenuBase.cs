using UnityEngine;
public abstract class MenuBase : MonoBehaviour
{
    public virtual void Init()
    {
        // Override this method to initialize the even when turned off
    }
    protected void ToMainMenu()
    {
        if (MenuManager.Instance.PreviousMenu == MenuManager.Instance.MainMenu)
        {
            MenuManager.SwitchMenu(MenuState.MainMenu);
            GameManager.ChangeGameState(GameState.Menu);
        }
        else
        {
            MenuManager.SwitchMenu(MenuState.Pause);
        }
    }

    protected virtual void ToControlsMenu()
    {
        MenuManager.SwitchMenu(MenuState.Controls);
    }

    protected virtual void ToSettingsMenu()
    {
        MenuManager.SwitchMenu(MenuState.Settings);
    }

    protected virtual void ToLevelSelectMenu()
    {
        MenuManager.SwitchMenu(MenuState.LevelSelect);
    }

    protected virtual void ToCreditsMenu()
    {
        MenuManager.SwitchMenu(MenuState.Credits);
    }

    protected virtual void ToPauseMenu()
    {
        MenuManager.SwitchMenu(MenuState.Pause);
        GameManager.ChangeGameState(GameState.Paused);
    }

    protected virtual void ToHUD()
    {
        MenuManager.SwitchMenu(MenuState.HUD);
        GameManager.ChangeGameState(GameState.Playing);
    }

    protected virtual void ToLevelMenu()
    {
        MenuManager.SwitchMenu(MenuState.LevelSelect);
    }

    protected virtual void QuitGame()
    {
        Application.Quit();
    }

    // public abstract void SelectFirst();
    public void SelectFirst()
    {
        MenuManager.Instance.EventSystem.SetSelectedGameObject(null);
    }

}