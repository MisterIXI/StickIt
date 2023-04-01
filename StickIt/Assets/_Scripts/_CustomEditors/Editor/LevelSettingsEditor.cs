using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelSettings))]
public class LevelSettingsEditor : Editor
{
    public static int LevelCount = 0;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var levelCount = EditorGUILayout.IntField("LevelCount", LevelCount);
        LevelCount = levelCount;
        LevelSettings levelSettings = (LevelSettings)target;
        if (GUILayout.Button("Add Levels"))
        {
            levelSettings.LevelCollection = new string[levelCount];
            for (int i = 0; i < levelCount; i++)
            {
                levelSettings.LevelCollection[i] = "Level" + (i + 1).ToString("D2");
            }
            EditorUtility.SetDirty(levelSettings);
        }

    }
}