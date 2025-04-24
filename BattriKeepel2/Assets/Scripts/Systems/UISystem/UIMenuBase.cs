using UnityEngine;

public abstract class UIMenuBase : MonoBehaviour
{
    [SerializeField] protected GameObject m_visual;

    public virtual void SetActive(bool state)
    {
        m_visual.SetActive(state);
    }

    public virtual void ToggleMenu()
    {
        SetActive(!m_visual.activeSelf);
    }
}
