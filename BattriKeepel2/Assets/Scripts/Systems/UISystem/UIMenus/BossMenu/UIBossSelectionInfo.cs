using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIBossSelectionInfo : UIMenuBase
{
    [SerializeField] Image bossMainVisual;
    [SerializeField] Button bossStartButton;
    [SerializeField] TMP_Text bossText;

    SO_BossSelectionInfos m_data;

    public void Init(SO_BossSelectionInfos data)
    {
        m_data = data;
        bossMainVisual.sprite = m_data.bossMainVisual;

        bossStartButton.onClick.AddListener(OnStartBossBtnClick);
        SetBossTesxtInfos(m_data);
    }

    public void LinkBossSelectionMenu(UIBossMenu menu)
    {
        menu.InvokeSelectionEnded();
    }


    void SetBossTesxtInfos(SO_BossSelectionInfos data)
    {
        bossText.text = $"<b><color=#{ColorUtility.ToHtmlStringRGB(data.bossTitleColor)}>{data.bossName}</b>\n\n{data.bossDesc}";
    }

    void OnStartBossBtnClick()
    {
        LevelLoader.LoadBossLevel(m_data.levelData);
        //start anim
    }

    void OnBossFightAnnimationend()
    {
        //anim event on end
    }
}
