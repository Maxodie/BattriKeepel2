using UnityEngine;

public class MainMenuLogger : Logger
{

}

public class MainMenuManager : GameManager
{
    [SerializeField] SO_UIBossMenu m_uiBossMenu;
    [SerializeField] Transform m_mainMenuCanvasTr;

    protected override void OnUIManagerCreated()
    {
        m_uiManager.GenerateUIData(m_uiBossMenu, m_mainMenuCanvasTr);
    }
}
