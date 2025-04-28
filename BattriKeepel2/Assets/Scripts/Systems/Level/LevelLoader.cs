using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public static class LevelLoader
{
    public static void LoadLevel(SO_LevelData data)
    {
        LoadLevelID(data);
    }

    static void LoadLevelID(SO_LevelData data)
    {
        UIDataResult result = UIManager.GenerateUIData(GameInstance.m_data, null);
        UILevelLoadingScreen loadingScreen = (UILevelLoadingScreen)result.Menu;
        loadingScreen.SetLoadingScreen(data.levelImage);

        int id = SceneUtility.GetBuildIndexByScenePath(data.path);
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
