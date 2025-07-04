using UnityEngine;
using System.Collections.Generic;

public class DebuggerTool : MonoBehaviour
{

#if DEVELOPMENT_BUILD || UNITY_EDITOR
    DebuggerToolNavigatorUI m_devToolNavigator;
    [SerializeField] DebuggerToolNavigatorUI m_devToolNavigatorPrefab;
    List<System.Type> m_activeUITools = new List<System.Type>();
    bool m_isActive = true;
    ProfilerStats m_rtProfiler = new();
    static bool exist = false;

    [SerializeField] InGameLogger inGameLogger;
#endif

    void Awake()
    {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
        if(!exist)
        {
            exist = true;
        }
        else
        {
            Destroy(gameObject);
        }

        if(!m_devToolNavigator)
        {
            m_devToolNavigator = Instantiate(m_devToolNavigatorPrefab, transform);
            m_devToolNavigator.Init(this);
            DontDestroyOnLoad(gameObject);
        }

        InGameLogger loggerVisual = Instantiate(inGameLogger, m_devToolNavigator.transform);
        loggerVisual.SetActiveLog(true);
        m_devToolNavigator.reopenBtn.onClick.AddListener(() => {loggerVisual.SetActiveLog(true);});
        m_devToolNavigator.closeBtn.onClick.AddListener(() => {loggerVisual.SetActiveLog(false);});

        GenerateDebuggerTab<DebuggertoolUIRTProfiler>();
        m_devToolNavigator.GenerateField<DebuggertoolUIRTProfiler>(m_rtProfiler, true);

        GraphicsManager.Get().OnVisualCreatedCallback.AddListener(UpdateTabs);
        Log.m_onLoggerCreated.AddListener(UpdateTabs);

#else

        Destroy(gameObject);
        return;
#endif
    }

#if DEVELOPMENT_BUILD || UNITY_EDITOR
    void UpdateTabs(object infos)
    {
        if(infos != null)
        {
            Logger infoLogger = infos as Logger;
            if(infoLogger != null)
            {
                foreach(Logger logger in Log.m_loggers)
                {
                    GenerateDebuggerTab<DebuggertoolUILogger>();
                    m_devToolNavigator.GenerateField<DebuggertoolUILogger>(logger);
                }
            }
        }
    }

    void Update()
    {
        foreach(DebuggerToolUIBase tool in m_devToolNavigator.m_tabDebuggersUI)
        {
            tool.Update();
        }
    }

    void GenerateDebuggerTab<T>() where T: DebuggerToolUIBase, new()
    {
        if(!m_activeUITools.Contains(typeof(T)))
        {
            m_devToolNavigator.AddDebuggerTab<T>();
            m_activeUITools.Add(typeof(T));
        }
    }

    public void SetActiveDebugger(bool state)
    {
        m_devToolNavigator.SetActiveDebugger(state);
        m_isActive = state;
    }
#endif
}
