using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public static class LevelLoader
{
    public static void LoadLevel(SO_LevelData data)
    {
        LoadLevelID(SceneUtility.GetBuildIndexByScenePath(data.path));
    }

    static void LoadLevelID(int id)
    {
        UIDataResult result = UIManager.GenerateUIData(GameInstance.m_data, null);
        result.Menu.StartCoroutine(LoadLevelAsync(StartOperation(id)));
    }

    static IEnumerator LoadLevelAsync(AsyncOperation operation)
    {
        while(!operation.isDone)
        {
            yield return null;
        }
    }

    static AsyncOperation StartOperation(int id)
    {
        return SceneManager.LoadSceneAsync(id);
    }
}
