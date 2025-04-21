using UnityEngine;

public class GameManagerLogger : Logger
{
}

public abstract class GameManager : GameEntityMonoBehaviour
{
    protected UIManager m_uiManager;

    protected virtual void Awake()
    {
        m_uiManager = new UIManager();
        OnUIManagerCreated();
    }

    protected virtual void Update()
    {

    }

    protected abstract void OnUIManagerCreated();
}
