using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(Level1))]
public class LevelEditor : Editor {
    Level1 level;
    Vector2 scrollPos;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        level = target as Level1;
        OnRulesGUI(level);
    }

    private void OnRulesGUI(Level1 level)
    {
        GUILayout.Label("Rules:");
        GUILayout.BeginVertical();
        for(int i = 0; i < level.Rules.Count; ++i)
        {
            EditorGUILayout.ObjectField(level.Rules[i], typeof(Unit),true);
        }
        GUILayout.EndVertical();

        if(GUILayout.Button("Add Rule"))
        {
            level.Rules.Add(new SpawnRule());
        }
    }
}
