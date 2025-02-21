using UnityEngine;

public class DebuggerToolUIField<T> : MonoBehaviour
{
    ReferenceContainer<T> value;
    public void SetValue(ReferenceContainer<T> o)
    {
        value = o;
    }
}

