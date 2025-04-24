using UnityEngine;

[CreateAssetMenu(fileName = "UIMainMenu", menuName = "MainMenu/UIMainMenu")]
public class SO_UIMainMenu : SO_UIData
{
    public UIMainMenu mainMenuPrefab;
    [HideInInspector] public string idleScenePath;
    [HideInInspector] public string startScenePath;
    public SO_UIBossMenu bossMenu;

    public override UIDataResult Init(Transform spawnParentTr)
    {
        UIMainMenu go = Object.Instantiate(mainMenuPrefab, spawnParentTr);
        go.Init(this);
        return new(go.gameObject);
    }
}
