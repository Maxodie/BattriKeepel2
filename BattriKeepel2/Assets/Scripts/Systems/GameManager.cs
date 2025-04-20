using UnityEngine;

public class GameManagerLogger : Logger
{
}

public class GameManager : GameEntityMonoBehaviour
{
    UIManager m_uiManager;
    [SerializeField] SO_UIBossMenu m_uiBossMenu;

    void Awake()
    {
        m_uiManager.GenerateUIData(m_uiBossMenu);
    }

    void Update()
    {

    }
}
