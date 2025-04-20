using UnityEngine;

public abstract class UIMenuBase : MonoBehaviour
{
    public abstract void Init<TUIData>(TUIData data) where TUIData : SO_UIData;
}
