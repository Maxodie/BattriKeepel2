using UnityEngine;
using System.Collections.Generic;

public class DebuggerToolUITabContent : MonoBehaviour
{
    [SerializeField] Transform m_fieldParent;
    [SerializeField] DebuggerToolUIFieldToggle m_toggleField;
    [SerializeField] DebuggerToolUIFieldDouble m_doubleField;

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

        Log.Warn<DebuggerLogger>("given field type is unknown : " + fieldType.Name);
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
