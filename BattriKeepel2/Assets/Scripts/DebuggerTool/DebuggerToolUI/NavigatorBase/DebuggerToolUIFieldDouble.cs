using UnityEngine;
using TMPro;

public class DebuggerToolUIFieldDouble : DebuggerToolUIField
{
    [SerializeField] TMP_InputField m_field;

    protected override void InitUI()
    {
        m_field.onValueChanged.AddListener(UpdateProperty);
    }

    protected override void SetUIValue()
    {
        m_field.text = PropertyField.GetValue(m_obj).ToString();
    }

    public virtual void UpdateProperty(string value)
    {
        SetValue(System.Double.Parse(value));
    }
}
