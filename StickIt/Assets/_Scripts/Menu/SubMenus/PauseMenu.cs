using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MenuBase
{
    [field: SerializeField] public Button ResumeButton { get; private set; }
    [field: SerializeField] public Button RetryButton { get; private set; }
    [field: SerializeField] public Button SettingsButton { get; private set; }
    [field: SerializeField] public Button ControlsButton { get; private set; }
    [field: SerializeField] public Button CreditsButton { get; private set; }
    [field: SerializeField] public Button BackToMenuButton { get; private set; }


    public override void Init()
    {
        ResumeButton.onClick.AddListener(ResumeGame);
        RetryButton.onClick.AddListener(RetryLevel);
        SettingsButton.onClick.AddListener(ToSettingsMenu);
        ControlsButton.onClick.AddListener(ToControlsMenu);
        CreditsButton.onClick.AddListener(ToCreditsMenu);
        BackToMenuButton.onClick.AddListener(BackToMainMenu);
    }


    private void OnEnable()
    {
        GameManager.ChangeGameState(GameState.Paused);
    }

    private void ResumeGame()
    {
        MenuManager.SwitchMenu(MenuState.HUD);
        GameManager.ChangeGameState(GameState.Playing);
    }

    private void RetryLevel()
    {
        GameManager.RetryLevel();
    }

    private void BackToMainMenu()
    {
        GameManager.LoadMainMenu();
    }

    // public override void SelectFirst()
    // {
    //     ResumeButton.Select();
    // }
}