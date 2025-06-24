using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIBossSelectionInfo : UIMenuBase
{
    [SerializeField] Image bossMainVisual;
    public Button bossStartButton;
    [SerializeField] TMP_Text bossText;

    SO_BossSelectionInfos m_data;
    UIBossMenu m_menu;

    public void Init(SO_BossSelectionInfos data)
    {
        m_data = data;
        bossMainVisual.sprite = m_data.bossMainVisual;

        bossStartButton.onClick.AddListener(OnStartBossBtnClick);
        SetBossTesxtInfos(m_data);
    }

    public void LinkBossMenu(UIBossMenu menu)
    {
        m_menu = menu;
    }

    void SetBossTesxtInfos(SO_BossSelectionInfos data)
    {
        bossText.text = $"<b><color=#{ColorUtility.ToHtmlStringRGB(data.bossTitleColor)}>{data.bossName}</b>\n\n{data.bossDesc}";
    }

    void OnStartBossBtnClick()
    {
        GameInstance.SetCurrentBossLevel(m_data.levelData);
        m_menu.InvokeSelectionEnded();
        //start anim
    }

    void OnBossFightAnnimationend()
    {
        //anim event on end
    }
}
