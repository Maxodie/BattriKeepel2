using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIMainMenu : UIMenuBase
{
    [SerializeField] Button m_startBtn;
    [SerializeField] Button m_idleBtn;

    public void Init(SO_UIMainMenu data)
    {
        UIDataResult bossMenu = UIManager.GenerateUIData(data.bossMenu, transform);

        m_idleBtn.onClick.AddListener(() => SceneManager.LoadScene(data.idleScenePath));
        m_startBtn.onClick.AddListener(() => { bossMenu.Go.SetActive(bossMenu.Go.activeSelf); });
    }
}
