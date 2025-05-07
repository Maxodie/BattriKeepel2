#if DEVELOPMENT_BUILD || UNITY_EDITOR
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DebuggerToolNavigatorUI : MonoBehaviour
{
    DebuggerTool m_debuggerTool;

    List<GameObject> m_tabContents = new();
    [HideInInspector] public List<DebuggerToolUIBase> m_tabDebuggersUI = new();
    List<GameObject> m_navigationTab = new();

    [Header("UI Prefabs")]
    [SerializeField] DebuggerNavigatorTab m_navigationTabPrefab;
    [SerializeField] DebuggerToolUITabContent m_tabContentPrefab;

    [Header("UI Parents")]
    [SerializeField] Transform m_navigationTabTransform;
    [SerializeField] Transform m_tabContentTransform;
    public Transform m_navigatorMain;

    [Header("Var")]
    public Button closeBtn;
    public Button reopenBtn;

    public void Init(DebuggerTool debuggerTool)
    {
        m_debuggerTool = debuggerTool;

        closeBtn.onClick.AddListener(CloseDebugger);
        reopenBtn.onClick.AddListener(OpenDebugger);
        reopenBtn.gameObject.SetActive(false);
        closeBtn.gameObject.SetActive(true);
    }

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

        newNavigationTab.SetNavigatorTabTitle(typeof(UI).ToString());
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

        foreach(DebuggerToolUIBase ui in m_tabDebuggersUI)
        {
            ui.Update();
        }

        m_tabContents[tabContentID].SetActive(true);
    }

    void GenerateTabContent<UI>() where UI: DebuggerToolUIBase, new()
    {
        UI uiBase = new UI();
        m_tabDebuggersUI.Add(uiBase);
        m_tabContents.Add(uiBase.Init(m_tabContentPrefab, m_tabContentTransform));
    }

    public void GenerateField<UI>(object script, bool readOnly = false) where UI: DebuggerToolUIBase, new()
    {
        DebuggerToolUIBase uiBase = m_tabDebuggersUI.Find(delegate(DebuggerToolUIBase item){ return item is UI; });
        if(uiBase == null)
        {
            Log.Warn<DebuggerLogger>("Could not find the debugger tab : " + typeof(UI).FullName);
            return;
        }

        uiBase.GenerateFields(script, readOnly);
    }

    public void CloseDebugger()
    {
        m_debuggerTool.SetActiveDebugger(false);
        reopenBtn.gameObject.SetActive(true);
        closeBtn.gameObject.SetActive(false);
    }

    public void OpenDebugger()
    {
        m_debuggerTool.SetActiveDebugger(true);
        reopenBtn.gameObject.SetActive(false);
        closeBtn.gameObject.SetActive(true);
    }

    public void SetActiveDebugger(bool state)
    {
        m_navigatorMain.gameObject.SetActive(state);
    }
}
#endif
