using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSelectionMenu", menuName = "UIManager/PlayerSelectionMenu")]
public class SO_UIPlayerSelectionMenu : SO_UIData
{
    public SO_PlayerData[] playerDatas;
    public UIPlayerSelectionInfo playerSelectionInfos;
    public UIButton playerSelectionNavigation;
    public UIPlayerSelectionMenu menuBasePrefab;
    public AudioClip selectSound;

    public override UIDataResult Init(Transform spawnParentTr)
    {
        UIPlayerSelectionMenu playerMenu = Object.Instantiate(menuBasePrefab, spawnParentTr);
        playerMenu.Init(this);
        return new(playerMenu.gameObject, playerMenu);
    }
}
