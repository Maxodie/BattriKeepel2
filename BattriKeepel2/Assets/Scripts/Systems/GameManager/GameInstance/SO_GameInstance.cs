using UnityEngine;

[CreateAssetMenu(fileName = "GameInstance", menuName = "GameManager/GameInstance")]
public class SO_GameInstance : SO_UIData
{
    public UILevelLoadingScreen levelLoader;

    public override UIDataResult Init(Transform spawnParentTr)
    {
        UILevelLoadingScreen menu;
        if(spawnParentTr)
        {
            menu = Object.Instantiate(levelLoader, spawnParentTr);
        }
        else
        {
            menu = Object.Instantiate(levelLoader);
        }

        return new(levelLoader.gameObject, menu);
    }
}
