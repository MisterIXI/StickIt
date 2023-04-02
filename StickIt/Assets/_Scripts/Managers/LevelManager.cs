using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    public static HashSet<string> SolvedLevels { get; private set; }
    private LevelSettings _levelSettings;
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
        SolvedLevels = new HashSet<string>();
        LoadProgressFromPrefs();
    }

    public static void MarkCurrentLevelAsComplete()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SolvedLevels.Add(currentSceneName);
        PlayerPrefs.SetInt(currentSceneName, 1);
    }

    public static void ClearProgress()
    {
        PlayerPrefs.DeleteAll();
        Instance.LoadProgressFromPrefs();
    }

    private void LoadProgressFromPrefs()
    {
        SolvedLevels.Clear();
        foreach (var level in _levelSettings.LevelCollection)
        {
            if (PlayerPrefs.GetInt(level, 0) == 1)
                SolvedLevels.Add(level);
        }
    }

    public static bool IsLevelSolvedShortName(string levelShortName)
    {
        char levelChar = levelShortName[0];
        string levelName = levelShortName.Substring(1);
        switch (levelChar)
        {
            case 'L':
                levelName = "Level" + levelName;
                break;
            case 'B':
                levelName = "Bonus" + levelName;
                break;
            default:
                Debug.LogError("Invalid level name: " + levelShortName);
                break;
        }
        return SolvedLevels.Contains(levelName);
    }
    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
            SolvedLevels = null;
        }
    }
}