using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class LevelMenu : MenuBase
{
    [field: SerializeField] public Button ResetProgressButton { get; private set; }
    [field: SerializeField] public Button BackToMenuButton { get; private set; }
    [field: SerializeField] public Button LevelButtonPrefab { get; private set; }
    [field: SerializeField] public RectTransform LevelButtonContainer { get; private set; }
    private Button[] _levelButtons;
    private LevelSettings _levelSettings;
    private TextMeshProUGUI _resetButtonText;
    private bool _resetPressedOnce = false;
    public override void Init()
    {
        _levelSettings = SettingsManager.Instance.LevelSettings;
        BackToMenuButton.onClick.AddListener(ToMainMenu);
        _levelButtons = new Button[_levelSettings.LevelCollection.Length];
        for (int i = 0; i < _levelSettings.LevelCollection.Length; i++)
        {
            int levelIndex = i;
            _levelButtons[i] = Instantiate(LevelButtonPrefab, transform);
            char levelChar = _levelSettings.LevelCollection[i][0];
            _levelButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = levelChar + (i + 1).ToString("D2");
            _levelButtons[i].onClick.AddListener(() =>
            {
                Debug.Log($"Loading level with id: {levelIndex}; length: {_levelSettings.LevelCollection.Length}");
                LoadLevel(_levelSettings.LevelCollection[levelIndex]);
            });
            _levelButtons[i].transform.SetParent(LevelButtonContainer);
        }
        _resetButtonText = ResetProgressButton.GetComponentInChildren<TextMeshProUGUI>();
        ResetProgressButton.onClick.AddListener(OnResetButtonPressed);
    }
    private void LoadLevel(string name)
    {
        GameManager.LoadLevel(name);
        GameManager.ChangeGameState(GameState.Playing);
    }

    private void OnEnable()
    {
        int completedLevels = LevelManager.SolvedLevels.Count;
        int levelUnlockSteps = completedLevels / 3;
        for (int i = 0; i < _levelButtons.Length; i++)
        {
            if (i < (levelUnlockSteps + 1) * 5)
            {
                _levelButtons[i].interactable = true;
            }
            else
            {
                _levelButtons[i].interactable = false;
            }
            if (LevelManager.IsLevelSolvedShortName(_levelButtons[i].GetComponentInChildren<TextMeshProUGUI>().text))
            {
                _levelButtons[i].GetComponentInChildren<Image>().color = Color.green;
            }
            else
            {
                _levelButtons[i].GetComponentInChildren<Image>().color = Color.white;
            }
        }
        _resetPressedOnce = false;
        _resetButtonText.text = "Reset Progress";
    }

    private void OnResetButtonPressed()
    {
        if (!_resetPressedOnce)
        {
            _resetPressedOnce = true;
            _resetButtonText.text = "Are you sure?";
        }
        else
        {
            // erase all progress
            LevelManager.ClearProgress();
            _resetPressedOnce = false;
            ToMainMenu();
        }
    }

    // public override void SelectFirst()
    // {
    //     BackToMenuButton.Select();
    // }
}