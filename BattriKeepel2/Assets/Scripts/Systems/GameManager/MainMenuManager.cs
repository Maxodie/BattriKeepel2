using UnityEngine;

public class MainMenuLogger : Logger
{

}

public class MainMenuManager : GameManager
{
    [SerializeField] SO_UIMainMenu m_uiMainMenu;
    [SerializeField] Transform m_mainMenuCanvasTr;

    protected override void OnUIManagerCreated()
    {
        UIDataResult mainMenu = UIManager.GenerateUIData(m_uiMainMenu, m_mainMenuCanvasTr);
        MobileEffect.VibrationEffect(MobileEffectVibration.BIG);
    }
}
