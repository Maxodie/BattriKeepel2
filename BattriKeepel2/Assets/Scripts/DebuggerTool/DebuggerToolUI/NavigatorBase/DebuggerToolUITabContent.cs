using UnityEngine;
public class DebuggerToolUITabContent : MonoBehaviour
{
    [SerializeField] Transform m_fieldParent;
    [SerializeField] DebuggerToolUIFieldToggle m_toggleField;

    public void AddField(System.Type fieldType)
    {
        if(fieldType == typeof(DebuggerToolUIFieldToggle))
        {
            Instantiate(m_toggleField, m_fieldParent);
            return;
        }
        //else if()

        Log.Warn<DebuggerLogger>("given field type is unknown");
    }
}
