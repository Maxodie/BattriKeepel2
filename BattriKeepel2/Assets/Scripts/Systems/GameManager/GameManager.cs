using UnityEngine;

public class GameManagerLogger : Logger
{
}

public abstract class GameManager : GameEntityMonoBehaviour
{
    [SerializeField] SO_GameInstance m_dataInstance;

    protected virtual void Awake()
    {
        GameInstance.StartGameInstance(m_dataInstance);
        OnUIManagerCreated();
    }

    protected virtual void Update()
    {

    }

    protected abstract void OnUIManagerCreated();
}
