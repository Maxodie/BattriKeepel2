using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : UIMenuBase
{
    [SerializeField] Button m_startBtn;
    [SerializeField] Button m_idleBtn;

    UIPlayerSelectionMenu m_playerSelectionMenu;

    public void Init(SO_UIMainMenu data)
    {
        UIDataResult bossMenu = UIManager.GenerateUIData(data.bossMenu, transform);

        UIBossMenu uiBossMenu = (UIBossMenu)bossMenu.Menu;
        uiBossMenu.BindMainMenu(this);
        uiBossMenu.BindOnBossSelectionEnded(OnBossSelectionEnded);

        m_playerSelectionMenu = (UIPlayerSelectionMenu)UIManager.GenerateUIData(data.playerSelectionMenu, transform).Menu;
        m_playerSelectionMenu.BindMainMenu(this);
        m_playerSelectionMenu.BindOnPlayerSelectionEnded(OnPlayerSelectionEnded);

        m_idleBtn.onClick.AddListener(() => LevelLoader.LoadLevel(data.idleSceneData));
        m_startBtn.onClick.AddListener(() => { bossMenu.Menu.ToggleMenu(); });
    }

    public void OnBossSelectionEnded()
    {
        m_playerSelectionMenu.SetActive(true);
    }

    public void OnPlayerSelectionEnded()
    {
        LevelLoader.LoadSelectedGameLevel();
    }
}
