using UnityEngine;

public static class UIManager
{
    public static UIDataResult GenerateUIData(SO_UIData data, Transform spawnCanvasTr)
    {
        return data.Init(spawnCanvasTr);
    }
}
