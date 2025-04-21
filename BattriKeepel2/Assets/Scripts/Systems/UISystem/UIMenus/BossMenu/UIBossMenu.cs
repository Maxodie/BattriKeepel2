using UnityEngine;
using UnityEngine.UI;

public class UIBossMenu : UIMenuBase
{
    [SerializeField] Button StartBossBtn;

    public override void Init<TUIData>(TUIData data)
    {
        StartBossBtn.onClick.AddListener(OnStartBossBtnClick);
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
