using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CharacterLevelDataSO))]
public class LevelDataEditor : Editor
{
    Vector2 scroll;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.Space();

        CharacterLevelDataSO levelData = (CharacterLevelDataSO)target;

        scroll = EditorGUILayout.BeginScrollView(scroll, GUILayout.MaxHeight(300));
        for (int i = 1; i < 41; i++)
        {
            EditorGUILayout.BeginHorizontal("box");
            EditorGUILayout.LabelField("Level" + (i));
            EditorGUILayout.LabelField((int)levelData.experienceLevelCurve.Evaluate(i) + "XP");
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.EndScrollView();
    }
}
