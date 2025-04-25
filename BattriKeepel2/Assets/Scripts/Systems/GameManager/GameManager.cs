using UnityEngine;

public class GameManagerLogger : Logger
{
}

public abstract class GameManager : GameEntityMonoBehaviour
{
    [SerializeField] SO_GameInstance m_gameInstance;

    protected virtual void Awake()
    {
        GameInstance.StartGameInstance(m_gameInstance);
        OnUIManagerCreated();
    }

    protected virtual void Update()
    {

    }

    protected abstract void OnUIManagerCreated();
}
