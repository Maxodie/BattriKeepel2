using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGameTransition : UIMenuBase
{
    [SerializeField] TMP_Text m_phaseName;
    [SerializeField] Image m_phaseVisual;

    [SerializeField] TMP_Text m_playerName;
    [SerializeField] Image m_playerVisual;

    public void Init(SO_UIData uiData)
    {
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
        Destroy(gameObject);
    }
}
