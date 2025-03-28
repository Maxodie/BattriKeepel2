using UnityEngine;
using System.Collections.Generic;

public class DebuggerTool : MonoBehaviour
{
    DebuggerToolNavigatorUI m_devToolNavigator;
    [SerializeField] DebuggerToolNavigatorUI m_devToolNavigatorPrefab;
    List<DebuggerToolBase> m_activeTools = new List<DebuggerToolBase>();
    List<System.Type> m_activeUITools = new List<System.Type>();
    bool isActive = true;

    void Awake()
    {
        if(!m_devToolNavigator)
        {
            m_devToolNavigator = Instantiate(m_devToolNavigatorPrefab, transform);
            DontDestroyOnLoad(gameObject);

            CreateActiveTool<RTProfiler>();
        }

        GenerateDebuggerTab<DebuggertoolUILogger>();
    }

    void Update()
    {
        if(!isActive) return;

        UpdateTabs(); //system d'entity et utpdate ça quand y'a un nouvel entity
    }

    void UpdateTabs()
    {
        foreach(Logger logger in Log.m_loggers)
        {
            m_devToolNavigator.GenerateField<DebuggertoolUILogger>(logger);
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
