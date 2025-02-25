using UnityEngine;
using System.Collections.Generic;

public class DebuggerToolNavigatorUI : MonoBehaviour
{
    List<GameObject> m_tabContents = new();
    List<DebuggerToolUIBase> m_tabDebuggersUI = new();
    List<GameObject> m_navigationTab = new();

    [Header("UI Prefabs")]
    [SerializeField] DebuggerNavigatorTab m_navigationTabPrefab;
    [SerializeField] DebuggerToolUITabContent m_tabContentPrefab;

    [Header("UI Parents")]
    [SerializeField] Transform m_navigationTabTransform;
    [SerializeField] Transform m_tabContentTransform;

    public void AddDebuggerTab<UI>(object script) where UI: DebuggerToolUIBase, new()
    {
        DebuggerNavigatorTab newNavigationTab = Instantiate(m_navigationTabPrefab, m_navigationTabTransform);
        m_navigationTab.Add(newNavigationTab.gameObject);
        int tabID = m_tabContents.Count;
        GenerateTabContent<UI>(script);

        newNavigationTab.AddNavigationTabBtnListener(
            delegate()
            {
                OnSwitchTab(tabID);
            }
        );

        OnSwitchTab(tabID);
    }

    void OnSwitchTab(int tabContentID)
    {
        foreach(GameObject tab in m_tabContents)
        {
            tab.SetActive(false);
        }

        m_tabContents[tabContentID].SetActive(true);
    }

    void GenerateTabContent<UI>(object script) where UI: DebuggerToolUIBase, new()
    {
        DebuggerToolUITabContent content = Instantiate(m_tabContentPrefab, m_tabContentTransform);
        m_tabContents.Add(content.gameObject);

        UI uiTool = new UI();
        uiTool.CreateUI(content, script);
        m_tabDebuggersUI.Add(uiTool);
    }
}
