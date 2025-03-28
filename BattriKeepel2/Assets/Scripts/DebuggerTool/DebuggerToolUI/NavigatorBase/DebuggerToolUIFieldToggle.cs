using UnityEngine.UI;
using UnityEngine;

public class DebuggerToolUIFieldToggle : DebuggerToolUIField
{
    [SerializeField] Toggle m_toogle;

    protected override void InitUI()
    {
        m_toogle.onValueChanged.AddListener(UpdateProperty);
    }

    public void UpdateProperty(bool value)
    {
        SetValue(value);
    }
}
