using UnityEngine;

[CreateAssetMenu(fileName = "BossMenu", menuName = "UIManager/BossMenu")]
public class SO_UIBossMenu : SO_UIData
{
    public SO_BossSelectionInfos[] bossSelectionInfos;
    public UIButton bossSelectionNavigation;
    public UIBossMenu menuBasePrefab;

    public override UIDataResult Init(Transform spawnParentTr)
    {
        UIBossMenu bossMenu = Object.Instantiate(menuBasePrefab, spawnParentTr);
        bossMenu.Init(this);
        return new(bossMenu.gameObject, bossMenu);
    }
}
