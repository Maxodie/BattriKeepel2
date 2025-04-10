using UnityEngine;
using UnityEngine.Events;

public class GraphicsManager
{
    static GraphicsManager s_instance;

    public UnityEvent<GameEntityGraphics> OnVisualCreatedCallback;

    public static GraphicsManager Get()
    {
        if(s_instance == null)
        {
            s_instance = new();
        }

        return s_instance;
    }

    public TGraphicsScript GenerateVisualInfos<TGraphicsScript>(GameEntityGraphics graphicsPrefab, Transform transform, bool isChild = true, bool dontDestroyOnLoad = false) where TGraphicsScript : GameEntityGraphics
    {
        TGraphicsScript result = UnityEngine.Object.Instantiate<TGraphicsScript>((TGraphicsScript)graphicsPrefab, transform, true);

        VisualInfosSpawnSetup(result, isChild, dontDestroyOnLoad);

        if(OnVisualCreatedCallback != null)
        {
            OnVisualCreatedCallback.Invoke(result);
        }

        return result;
    }

    public GameObject GenerateVisualInfos(GameObject graphicsPrefab, Transform transform, bool isChild = true, bool dontDestroyOnLoad = false)
    {
        GameObject result = UnityEngine.Object.Instantiate(graphicsPrefab, transform);

        VisualInfosSpawnSetup(result, isChild, dontDestroyOnLoad);

        if(OnVisualCreatedCallback != null)
        {
            OnVisualCreatedCallback.Invoke(null);
        }

        return result;
    }

    private void VisualInfosSpawnSetup<TGraphicsScript>(TGraphicsScript graphicsInfos, bool isChild, bool dontDestroyOnLoad) where TGraphicsScript : GameEntityGraphics
    {
        if(!isChild)
        {
            graphicsInfos.transform.SetParent(null);
        }
        if(dontDestroyOnLoad)
        {
            UnityEngine.Object.DontDestroyOnLoad(graphicsInfos);
        }
    }

    private void VisualInfosSpawnSetup(GameObject graphicsInfos, bool isChild, bool dontDestroyOnLoad)
    {
        if(!isChild)
        {
            graphicsInfos.transform.SetParent(null);
        }
        if(dontDestroyOnLoad)
        {
            UnityEngine.Object.DontDestroyOnLoad(graphicsInfos);
        }
    }
}
