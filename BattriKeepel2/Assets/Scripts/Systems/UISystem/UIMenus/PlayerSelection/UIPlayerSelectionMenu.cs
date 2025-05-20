using UnityEngine;
using UnityEngine.Events;

public class UIPlayerSelectionMenu : UIMenuBase
{
    [SerializeField] Transform m_playerSelectionInfoContent;
    [SerializeField] Transform m_playerSelectionNavigationContent;

    UIPlayerSelectionInfo[] navigationsPanels;
    int currentMenuID = -1;

    UnityEvent m_onPlayerSelectionEnded = new();

    public void Init(SO_UIPlayerSelectionMenu data)
    {
        navigationsPanels = new UIPlayerSelectionInfo[data.playerDatas.Length];
        for(int i=0; i < data.playerDatas.Length; i++)
        {
            int iCopy = i;
            UIPlayerSelectionInfo result = Instantiate(data.playerSelectionInfos, m_playerSelectionInfoContent);
            result.Init(data.playerDatas[i]);
            navigationsPanels[i] = result;

            Object.Instantiate(data.playerSelectionNavigation, m_playerSelectionNavigationContent).onClick.AddListener(() => { ChangeNavigationMenu(iCopy); });
        }

        ChangeNavigationMenu(0);
        SetActive(false);
    }

    public void BindMainMenu(UIMainMenu mainMenu)
    {
        for(int i=0; i < navigationsPanels.Length; i++)
        {
            navigationsPanels[i].LinkPlayerSelectionMenu(this);
        }
    }

    public void BindOnPlayerSelectionEnded(UnityAction action)
    {
        m_onPlayerSelectionEnded.AddListener(action);
    }

    public void OnPlayerSelectionEnd()
    {
        m_onPlayerSelectionEnded.Invoke();
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
