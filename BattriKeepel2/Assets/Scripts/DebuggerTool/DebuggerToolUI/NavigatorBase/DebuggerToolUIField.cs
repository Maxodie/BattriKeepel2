using UnityEngine;

public abstract class DebuggerToolUIField : MonoBehaviour
{
    System.Reflection.PropertyInfo m_property;
    object m_obj;

    public void Init(System.Reflection.PropertyInfo property, object obj)
    {
        m_property = property;
        m_obj = obj;

        InitUI();
    }

    protected abstract void InitUI();

    protected void SetValue(object value)
    {
        m_property.SetValue(m_obj, value);
    }
}

