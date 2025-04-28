using UnityEngine;
using System.Collections.Generic;

#if DEVELOPMENT_BUILD || UNITY_EDITOR
public class DebuggerToolUITabContent : MonoBehaviour
{
    [SerializeField] Transform m_fieldParent;
    [SerializeField] DebuggerToolUIFieldToggle m_toggleField;
    [SerializeField] DebuggerToolUIFieldDouble m_doubleField;
    [SerializeField] DebuggerToolUIFieldLong m_longField;
    [SerializeField] DebuggerToolUIFieldInt m_intField;

    List<DebuggerToolUIField> fields = new();

    public void AddField(System.Type fieldType, System.Reflection.PropertyInfo property, object script)
    {
        if(fieldType == typeof(bool))
        {
            DebuggerToolUIFieldToggle toggle = Instantiate(m_toggleField, m_fieldParent);
            toggle.Init(property, script, script.GetType().FullName + " " + property.Name);
            fields.Add(toggle);
            return;
        }
        else if(fieldType == typeof(double))
        {
            DebuggerToolUIFieldDouble doubleField = Instantiate(m_doubleField, m_fieldParent);
            doubleField.Init(property, script, script.GetType().FullName + " " + property.Name);
            fields.Add(doubleField);
            return;
        }
        else if(fieldType == typeof(long))
        {
            DebuggerToolUIFieldLong longFiled = Instantiate(m_longField, m_fieldParent);
            longFiled.Init(property, script, script.GetType().FullName + " " + property.Name);
            fields.Add(longFiled);
            return;
        }
        else if(fieldType == typeof(int))
        {
            DebuggerToolUIFieldInt intField = Instantiate(m_intField, m_fieldParent);
            intField.Init(property, script, script.GetType().FullName + " " + property.Name);
            fields.Add(intField);
            return;
        }

        Log.Warn<DebuggerLogger>("given field type is unknown : " + fieldType.Name);
    }

    void Update()
    {
        foreach(DebuggerToolUIField field in fields)
        {
             field.Update();
        }
    }

    public bool ContainField(System.Reflection.PropertyInfo property)
    {
        foreach(DebuggerToolUIField field in fields)
        {
            if(field.PropertyField == property)
            {
                return true;
            }
        }
        return false;
    }
}
#endif
