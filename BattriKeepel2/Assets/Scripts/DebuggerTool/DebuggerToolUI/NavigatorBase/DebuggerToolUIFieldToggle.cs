using UnityEngine.UI;
using UnityEngine;

public class DebuggerToolUIFieldToggle : DebuggerToolUIField
{
    [SerializeField] Toggle m_toogle;

    protected override void InitUI()
    {
        m_toogle.onValueChanged.AddListener(UpdateProperty);
    }

    protected override void SetUIValue()
    {
        m_toogle.isOn = (bool)PropertyField.GetValue(m_obj);
    }

    public void UpdateProperty(bool value)
    {
        SetValue(value);
    }
}
