#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(SO_GameLevelData))]
public class SO_BossLevelDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SO_GameLevelData script = (SO_GameLevelData)target;
        SceneAsset oldScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(script.path);

        SceneAsset newScene = EditorGUILayout.ObjectField("Scene Asset : ", oldScene, typeof(SceneAsset), false) as SceneAsset;

        if (EditorGUI.EndChangeCheck())
        {
            var newPath = AssetDatabase.GetAssetPath(newScene);

            if(script.path != newPath)
            {
                script.path = newPath;

                EditorUtility.SetDirty(script);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }

        }
        serializedObject.ApplyModifiedProperties();
    }
}

#endif
