using UnityEngine;
public abstract class MenuBase : MonoBehaviour
{
    protected void ToMainMenu()
    {
        MenuManager.SwitchMenu(MenuState.MainMenu);
        GameManager.ChangeGameState(GameState.Menu);
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

    public abstract void SelectFirst();

}