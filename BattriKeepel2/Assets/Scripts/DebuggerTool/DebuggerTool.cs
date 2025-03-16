using UnityEngine;
using System.Collections.Generic;

public class DebuggerTool : MonoBehaviour
{
    DebuggerToolNavigatorUI m_devToolNavigator;
    [SerializeField] DebuggerToolNavigatorUI m_devToolNavigatorPrefab;
    List<DebuggerToolBase> m_activeTools = new List<DebuggerToolBase>();
    List<System.Type> m_activeUITools = new List<System.Type>();

    void Awake()
    {
        if(!m_devToolNavigator)
        {
            m_devToolNavigator = Instantiate(m_devToolNavigatorPrefab, transform);
            DontDestroyOnLoad(gameObject);

            CreateActiveTool<RTProfiler>();
        }
    }

    void Update()
    {
        UpdateTabs();
        Log.Info("test : " + Log.m_loggers[0].IsActive);
    }

    void UpdateTabs()
    {

       // m_devToolNavigator.AddDebuggerTab<DebuggertoolUIRTProfiler>();
        if(Log.m_loggers[0] != null && !m_activeUITools.Contains(typeof(DebuggertoolUILogger)))
        {
            m_devToolNavigator.AddDebuggerTab<DebuggertoolUILogger>(Log.m_loggers[0]);
            m_activeUITools.Add(typeof(DebuggertoolUILogger));
        }
    }

    void CreateActiveTool<T>() where T: DebuggerToolBase, new()
    {
        DebuggerToolBase tool = m_activeTools.Find(delegate(DebuggerToolBase tool){ return tool is T; });
        if(tool == null)
        {
            m_activeTools.Add(new T());
            Log.Success<DebuggerLogger>($"Debugger tool of type : {typeof(T)} has been added");
        }
        else
        {
            Log.Warn<DebuggerLogger>($"Active debugger tool of type : {typeof(T)} already exist");
        }
    }

    void DestroyActiveTool<T>() where T: DebuggerToolBase
    {
        DebuggerToolBase tool = m_activeTools.Find(delegate(DebuggerToolBase tool){ return tool is T; });
        if(tool != null)
        {
            m_activeTools.Remove(tool);
            Log.Success<DebuggerLogger>($"Debugger tool of type : {typeof(T)} has been removed");
        }
        else
        {
            Log.Warn<DebuggerLogger>($"Could not find active debugger tool of type : {typeof(T)}");
        }
    }

    public void UpdateTools()
    {
        foreach (DebuggerToolBase tool in m_activeTools)
        {
            if(tool.IsToolActive)
            {
                tool.Update();
            }
        }
    }
}
