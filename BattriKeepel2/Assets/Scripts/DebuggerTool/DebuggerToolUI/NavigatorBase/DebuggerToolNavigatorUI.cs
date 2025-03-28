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

    public void AddDebuggerTab<UI>() where UI: DebuggerToolUIBase, new()
    {
        DebuggerToolUIBase uiBase = m_tabDebuggersUI.Find(delegate(DebuggerToolUIBase item){ return item is UI; });
        if(uiBase != null)
        {
            Log.Warn<DebuggerLogger>("Could not add a debugger tab");
        }

        DebuggerNavigatorTab newNavigationTab = Instantiate(m_navigationTabPrefab, m_navigationTabTransform);
        m_navigationTab.Add(newNavigationTab.gameObject);
        int tabID = m_tabContents.Count;

        GenerateTabContent<UI>();

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

    void GenerateTabContent<UI>() where UI: DebuggerToolUIBase, new()
    {
        UI uiBase = new UI();
        m_tabDebuggersUI.Add(uiBase);
        m_tabContents.Add(uiBase.Init(m_tabContentPrefab, m_tabContentTransform));
    }

    public void GenerateField<UI>(object script) where UI: DebuggerToolUIBase, new()
    {
        DebuggerToolUIBase uiBase = m_tabDebuggersUI.Find(delegate(DebuggerToolUIBase item){ return item is UI; });
        if(uiBase == null)
        {
            Log.Warn<DebuggerLogger>("Could not find the debugger tab : " + typeof(UI).FullName);
            return;
        }

        uiBase.GenerateFields(script);
    }

}
