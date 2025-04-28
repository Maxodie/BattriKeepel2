using UnityEngine;
using TMPro;

public abstract class DebuggerToolUIField : MonoBehaviour
{
    public System.Reflection.PropertyInfo PropertyField{set; get;}
    protected object m_obj;
    [SerializeField] TMP_Text m_text;

    public void Init(System.Reflection.PropertyInfo property, object obj, string fieldTitle)
    {
        m_text.text = fieldTitle;
        PropertyField = property;
        m_obj = obj;

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

