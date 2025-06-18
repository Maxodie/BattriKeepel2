using UnityEngine;
using UnityEngine.Events;

public enum SpawnDir
{
    North = 1 << 0,
    West = 1 << 1,
    East = 1 << 2,
    South = 1 << 3
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

    public Vector2 GetCameraLocation(int spawnDir)
    {
        Vector2 min = BoundsMin(Camera.main);
        Vector2 max = BoundsMax(Camera.main);

        int dir = (int)spawnDir;
        Vector3 result = Vector3.zero;

        if((dir &= (int)SpawnDir.North) != 0)
        {
            result.y = max.y;
        }

        if((dir &= (int)SpawnDir.West) != 0)
        {
            result.y = min.x;
        }

        if((dir &= (int)SpawnDir.East) != 0)
        {
            result.x = max.x;
        }

        if((dir &= (int)SpawnDir.South) != 0)
        {
            result.y = min.y;
        }

        return Vector3.zero;
    }

    public TGraphicsScript GenerateVisualInfos<TGraphicsScript>(GameEntityGraphics graphicsPrefab,
            SpawnDir spawnDir, Vector3 locationOffset, Quaternion rotation, IGameEntity owner, bool isChild = true,
            bool dontDestroyOnLoad = false) where TGraphicsScript : GameEntityGraphics
    {
        Vector3 location = GetCameraLocation((int)spawnDir);

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
