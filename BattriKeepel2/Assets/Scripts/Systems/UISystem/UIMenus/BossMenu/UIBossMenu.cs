using UnityEngine;

public class UIBossMenu : UIMenuBase
{
    [SerializeField] Transform m_bossSelectionInfoContent;
    [SerializeField] Transform m_bossSelectionNavigationContent;

    GameObject[] navigationsPanels;

    public void Init(SO_UIBossMenu data)
    {
        navigationsPanels = new GameObject[data.bossSelectionInfos.Length];
        for(int i=0; i < data.bossSelectionInfos.Length; i++)
        {
            int iCopy = i;
            UIDataResult result = data.bossSelectionInfos[i].Init(m_bossSelectionInfoContent);
            navigationsPanels[i] = result.Go;

            Object.Instantiate(data.bossSelectionNavigation, m_bossSelectionNavigationContent).onClick.AddListener(() => { ChangeNavigationMenu(iCopy); });
        }
    }

    void ChangeNavigationMenu(int id)
    {
        for(int i=0; i < navigationsPanels.Length; i++)
        {
            navigationsPanels[id].SetActive(i == id);
        }
    }
}
