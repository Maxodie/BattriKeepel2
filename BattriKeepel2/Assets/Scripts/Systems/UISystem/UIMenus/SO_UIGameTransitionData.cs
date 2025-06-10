using UnityEngine;

[CreateAssetMenu(fileName = "UIGameTransitionData", menuName = "Level/UIGameTransitionData")]
public class SO_UIGameTransitionData : SO_UIData
{
    public UIGameTransition transitionMenu;

    public override UIDataResult Init(Transform spawnParentTr)
    {
        UIGameTransition go = Object.Instantiate(transitionMenu, spawnParentTr);
        go.Init(this);
        return new(go.gameObject, go);
    }
}
