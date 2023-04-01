using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class LevelMenu : MenuBase
{
    [field: SerializeField] public Button BackToMenuButton { get; private set; }
    [field: SerializeField] public Button LevelButtonPrefab { get; private set; }
    [field: SerializeField] public RectTransform LevelButtonContainer { get; private set; }
    private Button[] _levelButtons;
    private LevelSettings _levelSettings;


    public override void Init()
    {
        _levelSettings = SettingsManager.Instance.LevelSettings;
        BackToMenuButton.onClick.AddListener(ToMainMenu);
        _levelButtons = new Button[_levelSettings.LevelCollection.Length];
        for (int i = 0; i < _levelSettings.LevelCollection.Length; i++)
        {
            int levelIndex = i;
            _levelButtons[i] = Instantiate(LevelButtonPrefab, transform);
            _levelButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = "" + (i + 1).ToString("D2");
            _levelButtons[i].onClick.AddListener(() =>
            {
                Debug.Log($"Loading level with id: {levelIndex}; length: {_levelSettings.LevelCollection.Length}");
                LoadLevel(_levelSettings.LevelCollection[levelIndex]);
            });
            _levelButtons[i].transform.SetParent(LevelButtonContainer);
        }
    }
    private void LoadLevel(string name)
    {
        GameManager.LoadLevel(name);
    }

    // public override void SelectFirst()
    // {
    //     BackToMenuButton.Select();
    // }
}