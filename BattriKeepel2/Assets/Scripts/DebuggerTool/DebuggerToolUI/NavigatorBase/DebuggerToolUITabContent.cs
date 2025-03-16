using UnityEngine;
using System.Collections.Generic;

public class DebuggerToolUITabContent : MonoBehaviour
{
    [SerializeField] Transform m_fieldParent;
    [SerializeField] DebuggerToolUIFieldToggle m_toggleField;

    List<DebuggerToolUIField> fields = new();

    public void AddField(System.Type fieldType, System.Reflection.PropertyInfo property, object script)
    {
        if(fieldType == typeof(bool))
        {
            DebuggerToolUIFieldToggle toggle = Instantiate(m_toggleField, m_fieldParent);
            toggle.Init(property, script);
            fields.Add(toggle);
            return;
        }
        //else if()

        Log.Warn<DebuggerLogger>("given field type is unknown : " + fieldType.Name);
    }
}
