using UnityEngine;

[CreateAssetMenu(fileName = "UIData", menuName = "UIManager/UIData")]
public abstract class SO_UIData : ScriptableObject
{
    public abstract UIDataResult Init(Transform spawnParentTr);
}

public class UIDataResult
{
    public UIDataResult(GameObject go)
    {
        Go = go;
    }

    public GameObject Go;
}
