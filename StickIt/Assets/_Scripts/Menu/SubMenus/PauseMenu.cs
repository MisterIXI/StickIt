using UnityEngine;

public class PauseMenu : MenuBase
{

    private void OnEnable()
    {
        GameManager.ChangeGameState(GameState.Paused);
    }

    private void ResumeGame()
    {
        MenuManager.SwitchMenu(MenuState.HUD);
        GameManager.ChangeGameState(GameState.Playing);
    }

    public override void SelectFirst()
    {
        throw new System.NotImplementedException();
    }
}