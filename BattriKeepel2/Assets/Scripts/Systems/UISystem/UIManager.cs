using UnityEngine;

public class UIManager
{
    public void GenerateUIData(SO_UIData data)
    {
        UIMenuBase instance = Object.Instantiate(data.menuBasePrefab);
        instance.Init(data);
    }
}
