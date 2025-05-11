using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : UIMenuBase
{
    [SerializeField] Button m_startBtn;
    [SerializeField] Button m_idleBtn;

    public void Init(SO_UIMainMenu data)
    {
        UIDataResult bossMenu = UIManager.GenerateUIData(data.bossMenu, transform);

        GameInstance.SetCurrentPlayerLevel(data.playerData);
        m_idleBtn.onClick.AddListener(() => LevelLoader.LoadLevel(data.idleSceneData));
        m_startBtn.onClick.AddListener(() => { bossMenu.Menu.ToggleMenu(); });
    }
}
