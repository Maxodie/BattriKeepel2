using UnityEngine;

public class DebuggerToolUIField<T> : MonoBehaviour
{
    ReferenceContainer<T> value;
    public void SetValue(ref T o)
    {
        value = new ReferenceContainer<T>(o);
    }
}

