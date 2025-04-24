using UnityEngine;

public class UIBossMenu : UIMenuBase
{
    [SerializeField] Transform m_bossSelectionInfoContent;
    [SerializeField] Transform m_bossSelectionNavigationContent;

    GameObject[] navigationsPanels;
    int currentMenuID = -1;

    public void Init(SO_UIBossMenu data)
    {
        navigationsPanels = new GameObject[data.bossSelectionInfos.Length];
        for(int i=0; i < data.bossSelectionInfos.Length; i++)
        {
            int iCopy = i;
            UIDataResult result = UIManager.GenerateUIData(data.bossSelectionInfos[i], m_bossSelectionInfoContent);
            navigationsPanels[i] = result.Go;

            Object.Instantiate(data.bossSelectionNavigation, m_bossSelectionNavigationContent).onClick.AddListener(() => { ChangeNavigationMenu(iCopy); });
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
