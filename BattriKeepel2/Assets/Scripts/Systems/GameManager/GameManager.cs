using UnityEngine;

public class GameManagerLogger : Logger
{
}

public abstract class GameManager : GameEntityMonoBehaviour
{
    protected UIManager m_uiManager;

    void Awake()
    {
        m_uiManager = new UIManager();
        OnUIManagerCreated();
    }

    void Update()
    {

    }

    protected abstract void OnUIManagerCreated();
}
