using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIBossSelectionInfo : UIMenuBase
{
    [SerializeField] Image bossMainVisual;
    [SerializeField] Button bossStartButton;
    [SerializeField] TMP_Text bossText;

    public void Init(SO_BossSelectionInfos data)
    {
        bossMainVisual.sprite = data.bossMainVisual;

        bossStartButton.onClick.AddListener(OnStartBossBtnClick);
        SetBossTesxtInfos(data);
    }

    void SetBossTesxtInfos(SO_BossSelectionInfos data)
    {
        bossText.text = $"<b><color=#{ColorUtility.ToHtmlStringRGB(data.bossTitleColor)}>{data.bossName}</b>\n\n{data.bossDesc}";
    }

    void OnStartBossBtnClick()
    {
        Log.Info("start boss");
        //start anim
    }

    void OnBossFightAnnimationend()
    {
        //anim event on end
    }
}
