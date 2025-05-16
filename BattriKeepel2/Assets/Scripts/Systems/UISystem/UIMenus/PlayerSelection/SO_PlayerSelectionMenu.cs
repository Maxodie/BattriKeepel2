using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "PlayerSelectionMenu", menuName = "UIManager/PlayerSelectionMenu")]
public class SO_UIPlayerSelectionMenu : SO_UIData
{
    public UIPlayerSelectionInfo[] playerSelectionInfos;
    public SO_PlayerData playerData;
    public Button playerSelectionNavigation;
    public UIPlayerSelectionMenu menuBasePrefab;

    public override UIDataResult Init(Transform spawnParentTr)
    {
        UIPlayerSelectionMenu bossMenu = Object.Instantiate(menuBasePrefab, spawnParentTr);
        bossMenu.Init(this);
        return new(bossMenu.gameObject, bossMenu);
    }
}
