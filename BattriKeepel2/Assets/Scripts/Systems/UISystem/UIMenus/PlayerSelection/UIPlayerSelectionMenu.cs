using UnityEngine;

public class UIPlayerSelectionMenu : UIMenuBase
{
    [SerializeField] Transform m_playerSelectionInfoContent;
    [SerializeField] Transform m_playerSelectionNavigationContent;

    UIPlayerSelectionInfo[] navigationsPanels;
    int currentMenuID = -1;

    public void Init(SO_UIPlayerSelectionMenu data)
    {
        navigationsPanels = new UIPlayerSelectionInfo[data.playerSelectionInfos.Length];
        for(int i=0; i < data.playerSelectionInfos.Length; i++)
        {
            int iCopy = i;
            UIPlayerSelectionInfo result = Instantiate(data.playerSelectionInfos[i], m_playerSelectionInfoContent);
            navigationsPanels[i] = result;

            Object.Instantiate(data.playerSelectionNavigation, m_playerSelectionNavigationContent).onClick.AddListener(() => { ChangeNavigationMenu(iCopy); });
        }

        ChangeNavigationMenu(0);
        SetActive(false);
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
