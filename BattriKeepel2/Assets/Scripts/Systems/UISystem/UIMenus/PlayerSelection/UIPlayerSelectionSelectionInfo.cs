using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIPlayerSelectionInfo : UIMenuBase
{
    Animation m_playerMainVisual;
    public Button m_selectButton;
    [SerializeField] TMP_Text m_nameText;

    SO_PlayerData m_data;
    UIPlayerSelectionMenu m_menu;

    public void Init(SO_PlayerData data)
    {
        m_data = data;
        m_playerMainVisual = m_data.selectionAnim;

        m_selectButton.onClick.AddListener(OnStartBossBtnClick);
        SetBossTesxtInfos(m_data);
    }

    public void LinkPlayerSelectionMenu(UIPlayerSelectionMenu menu)
    {
        m_menu = menu;
    }

    void SetBossTesxtInfos(SO_PlayerData data)
    {
        m_nameText.text = $"<b><color=#{ColorUtility.ToHtmlStringRGB(data.nameColor)}>{data.playerName}</b>\n\n{data.playerDesc}";
    }

    void OnStartBossBtnClick()
    {
        GameInstance.SetCurrentPlayerData(m_data);
        m_menu.OnPlayerSelectionEnd();
        //start anim
    }

    void OnBossFightAnnimationend()
    {
        //anim event on end
    }
}
