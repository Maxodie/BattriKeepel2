using UnityEngine;

public class MainMenuLogger : Logger
{

}

public class MainMenuManager : GameManager
{
    [SerializeField] SO_UIMainMenu m_uiMainMenu;
    [SerializeField] Transform m_mainMenuCanvasTr;
    [SerializeField] SO_GameInstance m_gameInstance;

    protected override void OnUIManagerCreated()
    {
        UIDataResult mainMenu = UIManager.GenerateUIData(m_uiMainMenu, m_mainMenuCanvasTr);
        GameInstance.StartGameInstance(m_gameInstance);
    }
}
