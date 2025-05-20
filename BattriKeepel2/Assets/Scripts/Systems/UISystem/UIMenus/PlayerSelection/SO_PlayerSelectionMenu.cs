using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "PlayerSelectionMenu", menuName = "UIManager/PlayerSelectionMenu")]
public class SO_UIPlayerSelectionMenu : SO_UIData
{
    public SO_PlayerData[] playerDatas;
    public UIPlayerSelectionInfo playerSelectionInfos;
    public Button playerSelectionNavigation;
    public UIPlayerSelectionMenu menuBasePrefab;

    public override UIDataResult Init(Transform spawnParentTr)
    {
        UIPlayerSelectionMenu playerMenu = Object.Instantiate(menuBasePrefab, spawnParentTr);
        playerMenu.Init(this);
        return new(playerMenu.gameObject, playerMenu);
    }
}
