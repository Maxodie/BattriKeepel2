using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelLoaderLogger : Logger
{

}

public static class LevelLoader
{
    public static void LoadLevel(SO_LevelData data)
    {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
        if(data.GetType() == typeof(SO_GameLevelData))
        {
            Log.Error("You are trying to load a boss level as a level data. It will work but i don't want you to do it. Use 'LevelBossData' instead");
        }
#endif
        LoadLevelID(data);
    }

    public static void LoadSelectedGameLevel()
    {
        if(!GameInstance.GetCurrentLevelData())
        {
            Log.Error<LevelLoaderLogger>("trying to load null level, please set gameInstance level data");
            return;
        }

        LoadLevelID(GameInstance.GetCurrentLevelData());
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
