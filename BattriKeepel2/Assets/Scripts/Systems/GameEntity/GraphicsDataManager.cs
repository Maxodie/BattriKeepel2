using UnityEngine;
using UnityEngine.Events;

public class GraphicsManager
{
    static GraphicsManager s_instance;

    public UnityEvent<GameEntityGraphics> OnVisualCreatedCallback = new();

    public static GraphicsManager Get()
    {
        if(s_instance == null)
        {
            s_instance = new();
        }

        return s_instance;
    }

    public TGraphicsScript GenerateVisualInfos<TGraphicsScript>(GameEntityGraphics graphicsPrefab,
            Vector2 position, Quaternion rotation, IGameEntity owner, bool isChild = true,
            bool dontDestroyOnLoad = false) where TGraphicsScript : GameEntityGraphics
    {
        TGraphicsScript result = UnityEngine.Object.Instantiate<TGraphicsScript>((TGraphicsScript)graphicsPrefab, position, rotation);

        VisualInfosSpawnSetup(result, owner, isChild, dontDestroyOnLoad);

        if(OnVisualCreatedCallback != null)
        {
            OnVisualCreatedCallback.Invoke(result);
        }

        return result;
    }

    public TGraphicsScript GenerateVisualInfos<TGraphicsScript>(GameEntityGraphics graphicsPrefab, Transform transform, IGameEntity owner, bool isChild = true, bool dontDestroyOnLoad = false) where TGraphicsScript : GameEntityGraphics
    {
        TGraphicsScript result = UnityEngine.Object.Instantiate<TGraphicsScript>((TGraphicsScript)graphicsPrefab, transform, false);

        VisualInfosSpawnSetup(result, owner, isChild, dontDestroyOnLoad);

        if(OnVisualCreatedCallback != null)
        {
            OnVisualCreatedCallback.Invoke(result);
        }

        return result;
    }

    public GameObject GenerateVisualInfos(GameObject graphicsPrefab, Transform transform, IGameEntity owner, bool isChild = true, bool dontDestroyOnLoad = false)
    {
        GameObject result = UnityEngine.Object.Instantiate(graphicsPrefab, transform);

        VisualInfosSpawnSetup(result, owner, isChild, dontDestroyOnLoad);

        if(OnVisualCreatedCallback != null)
        {
            OnVisualCreatedCallback.Invoke(null);
        }

        return result;
    }

    private void VisualInfosSpawnSetup<TGraphicsScript>(TGraphicsScript graphicsInfos, IGameEntity owner, bool isChild, bool dontDestroyOnLoad) where TGraphicsScript : GameEntityGraphics
    {
        graphicsInfos.Init(owner);

        if(!isChild)
        {
            graphicsInfos.transform.SetParent(null);
            graphicsInfos.transform.localScale = new Vector3(1, 1, 1);
        }

        if(dontDestroyOnLoad)
        {
            UnityEngine.Object.DontDestroyOnLoad(graphicsInfos);
        }
    }

    private void VisualInfosSpawnSetup(GameObject graphicsInfos, IGameEntity owner, bool isChild, bool dontDestroyOnLoad)
    {
        graphicsInfos.GetComponent<GameEntityGraphics>().Init(owner);

        if(!isChild)
        {
            graphicsInfos.transform.SetParent(null);
            graphicsInfos.transform.localScale = new Vector3(1, 1, 1);
        }

        if(dontDestroyOnLoad)
        {
            UnityEngine.Object.DontDestroyOnLoad(graphicsInfos);
        }
    }
}
