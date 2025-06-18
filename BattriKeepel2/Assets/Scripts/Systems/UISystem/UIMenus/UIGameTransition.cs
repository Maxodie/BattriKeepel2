using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIGameTransition : UIMenuBase
{
    [SerializeField] TMP_Text m_phaseName;
    [SerializeField] Image m_phaseVisual;

    [SerializeField] TMP_Text m_playerName;
    [SerializeField] Image m_playerVisual;

    UnityEvent m_onTransitionEnd = new();
    [SerializeField] Animator m_animator;

    public void Init(SO_UIData uiData)
    {
    }

    public void ActiveTransition(bool state)
    {
        SetActive(state);
        m_animator.SetBool("transition", state);
    }

    public void BindOnTransitionEnd(UnityAction action)
    {
        m_onTransitionEnd.AddListener(action);
    }

    public void SetTransition(SO_LevelPhase phaseData, SO_PlayerData playerData)
    {
        m_phaseName.text = phaseData.phaseDisplayName;
        m_phaseVisual.sprite = phaseData.phaseDisplayVisual;

        m_playerName.text = playerData.playerName;
        m_playerVisual.sprite = playerData.playerVisual;
    }

    public void OnTransitionEndEvent()
    {
        m_onTransitionEnd.Invoke();
    }
}
