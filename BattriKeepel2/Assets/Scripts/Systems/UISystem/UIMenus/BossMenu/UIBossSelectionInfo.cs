using UnityEngine;
using UnityEngine.UI;

public class UIBossSelectionInfo : UIMenuBase
{
    [SerializeField] Image bossMainVisual;
    [SerializeField] Button bossStartButton;

    public void Init(SO_BossSelectionInfos data)
    {
        bossMainVisual.sprite = data.bossMainVisual;

        bossStartButton.onClick.AddListener(OnStartBossBtnClick);
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
