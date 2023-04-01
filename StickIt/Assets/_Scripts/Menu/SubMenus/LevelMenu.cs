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
            _levelButtons[i] = Instantiate(LevelButtonPrefab, transform);
            _levelButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = "" + (i + 1);
            _levelButtons[i].onClick.AddListener(() => LoadLevel(_levelSettings.LevelCollection[i].name));
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