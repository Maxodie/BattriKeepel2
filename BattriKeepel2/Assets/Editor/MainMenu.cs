#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(SO_UIMainMenu))]
public class SO_UIMainMenuEditor : Editor
{
    public override void OnInspectorGUI()
    {

        DrawDefaultInspector();

        SO_UIMainMenu script = (SO_UIMainMenu)target;
        SceneAsset oldIdleScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(script.idleScenePath);
        SceneAsset oldStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(script.startScenePath);

        SceneAsset newIdleScene = EditorGUILayout.ObjectField("Idle-Hatching Scene", oldIdleScene, typeof(SceneAsset), false) as SceneAsset;
        SceneAsset newStartScene = EditorGUILayout.ObjectField("Start Scene", oldStartScene, typeof(SceneAsset), false) as SceneAsset;

        if (EditorGUI.EndChangeCheck())
        {
            var newIdlePath = AssetDatabase.GetAssetPath(newIdleScene);
            var newStartPath = AssetDatabase.GetAssetPath(newStartScene);

            if(script.idleScenePath != newIdlePath || script.startScenePath != newStartPath)
            {
                script.idleScenePath = newIdlePath;

                script.startScenePath = newStartPath;

                EditorUtility.SetDirty(script);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }

        }
        serializedObject.ApplyModifiedProperties();
    }
}

#endif
