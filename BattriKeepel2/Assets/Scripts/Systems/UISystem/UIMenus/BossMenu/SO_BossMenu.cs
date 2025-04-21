using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "BossMenu", menuName = "UIManager/BossMenu")]
public class SO_UIBossMenu : SO_UIData
{
    public SO_BossSelectionInfos[] bossSelectionInfos;
    public Button bossSelectionNavigation;
    public UIBossMenu menuBasePrefab;

    public override UIDataResult Init(Transform spawnParentTr)
    {
        UIBossMenu bossMenu = Object.Instantiate(menuBasePrefab, spawnParentTr);
        bossMenu.Init(this);
        return new(bossMenu.gameObject);
    }
}
