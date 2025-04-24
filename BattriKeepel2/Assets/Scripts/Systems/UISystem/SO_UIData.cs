using UnityEngine;

[CreateAssetMenu(fileName = "UIData", menuName = "UIManager/UIData")]
public abstract class SO_UIData : ScriptableObject
{
    public abstract UIDataResult Init(Transform spawnParentTr);
}

public class UIDataResult
{
    public UIDataResult(GameObject go, UIMenuBase menu)
    {
        Go = go;
        Menu = menu;
    }

    public GameObject Go;
    public UIMenuBase Menu;
}
