using UnityEngine;
using UnityEngine.Events;

public enum SpawnDir
{
    North,
    West,
    Est,
    South
}

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

    public Vector2 BoundsMin(Camera camera)
	{
		return (Vector2)camera.transform.position - Extents(camera);
	}

    public Vector2 BoundsMax(Camera camera)
	{
		return (Vector2)camera.transform.position + Extents(camera);
	}

    public Vector2 Extents(Camera camera)
	{
		if (camera.orthographic)
			return new Vector2(camera.orthographicSize * Screen.width/Screen.height, camera.orthographicSize);
		else
		{
			Log.Error("Camera is not orthographic!");
			return new Vector2();
		}
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

    public Vector2 GetCameraLocation(SpawnDir spawnDir)
    {
        Vector2 min = BoundsMin(Camera.main);
        Vector2 max = BoundsMax(Camera.main);
        switch(spawnDir)
        {
            case SpawnDir.North:
                return new Vector3(0.0f, max.y);

            case SpawnDir.West:
                return new Vector3(min.x, 0.0f);

            case SpawnDir.Est:
                return new Vector3(max.x, 0.0f);

            case SpawnDir.South:
                return new Vector3(0.0f, min.y);

            default:
                return new Vector3();
        }
    }

    public TGraphicsScript GenerateVisualInfos<TGraphicsScript>(GameEntityGraphics graphicsPrefab,
            SpawnDir spawnDir, Vector3 locationOffset, Quaternion rotation, IGameEntity owner, bool isChild = true,
            bool dontDestroyOnLoad = false) where TGraphicsScript : GameEntityGraphics
    {
        Vector3 location = GetCameraLocation(spawnDir);

        TGraphicsScript result = UnityEngine.Object.Instantiate<TGraphicsScript>((TGraphicsScript)graphicsPrefab, location + locationOffset, rotation);
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
        }

        if(dontDestroyOnLoad)
        {
            UnityEngine.Object.DontDestroyOnLoad(graphicsInfos);
        }
    }
}
