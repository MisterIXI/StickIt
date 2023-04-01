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

    public static void SolveCurrentLevel()
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
            if (PlayerPrefs.GetInt(level.name, 0) == 1)
                SolvedLevels.Add(level.name);
        }
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