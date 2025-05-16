using UnityEngine;
using UnityEngine.Events;

public class UIBossMenu : UIMenuBase
{
    [SerializeField] Transform m_bossSelectionInfoContent;
    [SerializeField] Transform m_bossSelectionNavigationContent;

    UnityEvent m_onBossSelectionEnded = new();

    UIBossSelectionInfo[] navigationsPanels;
    int currentMenuID = -1;

    public void Init(SO_UIBossMenu data)
    {
        navigationsPanels = new UIBossSelectionInfo[data.bossSelectionInfos.Length];
        for(int i=0; i < data.bossSelectionInfos.Length; i++)
        {
            int iCopy = i;
            UIDataResult result = UIManager.GenerateUIData(data.bossSelectionInfos[i], m_bossSelectionInfoContent);
            navigationsPanels[i] = (UIBossSelectionInfo)result.Menu;

            Object.Instantiate(data.bossSelectionNavigation, m_bossSelectionNavigationContent).onClick.AddListener(() => { ChangeNavigationMenu(iCopy); });
        }

        ChangeNavigationMenu(0);
        SetActive(false);
    }

    public void BindMainMenu(UIMainMenu mainMenu)
    {
        for(int i=0; i < navigationsPanels.Length; i++)
        {
            navigationsPanels[i].LinkBossSelectionMenu(this);
        }
    }

    public void InvokeSelectionEnded()
    {
        m_onBossSelectionEnded.Invoke();
    }

    public void BindOnBossSelectionEnded(UnityAction action)
    {
        m_onBossSelectionEnded.AddListener(action);
    }

    void ChangeNavigationMenu(int id)
    {
        if(id == currentMenuID)
        {
            return;
        }

        for(int i=0; i < navigationsPanels.Length; i++)
        {
            navigationsPanels[i].SetActive(i == id);
        }
        Log.Success<MainMenuLogger>("Boss selection menu id : " + id);
    }
}
