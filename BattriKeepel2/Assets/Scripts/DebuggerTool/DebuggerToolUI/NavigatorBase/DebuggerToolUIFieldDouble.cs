using UnityEngine.UI;
using UnityEngine;

public class DebuggerToolUIFieldDouble : DebuggerToolUIField
{
    [SerializeField] InputField m_field;

    protected override void InitUI()
    {
        m_field.onValueChanged.AddListener(UpdateProperty);
    }

    public void UpdateProperty(string value)
    {
        SetValue(System.Int32.Parse(value));
    }
}
