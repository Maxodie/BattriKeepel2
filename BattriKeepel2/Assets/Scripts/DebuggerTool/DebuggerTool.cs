using UnityEngine;
using System.Collections.Generic;

public class DebuggerTool : MonoBehaviour
{
    GameObject m_devToolGO;

    [SerializeField] GameObject m_devToolGOPrefab;
    List<DebuggerToolBase> m_activeTools = new List<DebuggerToolBase>();

    void Awake()
    {
        if(!m_devToolGO)
        {
            m_devToolGO = Instantiate(m_devToolGOPrefab);
            DontDestroyOnLoad(m_devToolGO);
        }
    }

    void CreateActiveTool<T>() where T: DebuggerToolBase, new()
    {
        DebuggerToolBase tool = m_activeTools.Find(delegate(DebuggerToolBase tool){ return tool is T; });
        if(tool == null)
        {
            m_activeTools.Add(new T());
            Log.Success($"Debugger tool of type : {typeof(T)} has been added");
        }
        else
        {
            Log.Warn($"Active debugger tool of type : {typeof(T)} already exist");
        }
    }

    void DestroyActiveTool<T>() where T: DebuggerToolBase
    {
        DebuggerToolBase tool = m_activeTools.Find(delegate(DebuggerToolBase tool){ return tool is T; });
        if(tool != null)
        {
            m_activeTools.Remove(tool);
            Log.Success($"Debugger tool of type : {typeof(T)} has been removed");
        }
        else
        {
            Log.Warn($"Could not find active debugger tool of type : {typeof(T)}");
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
