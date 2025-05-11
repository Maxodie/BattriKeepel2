using UnityEngine;

[CreateAssetMenu(fileName = "UIMainMenu", menuName = "MainMenu/UIMainMenu")]
public class SO_UIMainMenu : SO_UIData
{
    public UIMainMenu mainMenuPrefab;
    public SO_LevelData idleSceneData;
    public SO_LevelData startSceneData;
    public SO_UIBossMenu bossMenu;
    public SO_PlayerData playerData;

    public override UIDataResult Init(Transform spawnParentTr)
    {
        UIMainMenu go = Object.Instantiate(mainMenuPrefab, spawnParentTr);
        go.Init(this);
        return new(go.gameObject, go);
    }
}
