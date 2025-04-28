using UnityEngine;
using TMPro;

public abstract class DebuggerToolUIField : MonoBehaviour
{
    public System.Reflection.PropertyInfo PropertyField{set; get;}
    protected object m_obj;
    [SerializeField] TMP_Text m_text;
    bool m_readOnly;

    public void Init(System.Reflection.PropertyInfo property, object obj, string fieldTitle, bool readOnly)
    {
        m_text.text = fieldTitle;
        PropertyField = property;
        m_obj = obj;
        m_readOnly = readOnly;

        SetValue(property.GetValue(obj));
        InitUI();
        SetUIValue();
    }

    protected abstract void InitUI();
    protected abstract void SetUIValue();

    public void Update()
    {
        SetUIValue();
    }

    protected void SetValue(object value)
    {
        PropertyField.SetValue(m_obj, value);
    }
}

