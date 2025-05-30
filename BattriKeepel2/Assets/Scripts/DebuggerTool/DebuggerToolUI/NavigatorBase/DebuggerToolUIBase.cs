#if DEVELOPMENT_BUILD || UNITY_EDITOR

using UnityEngine;

public abstract class DebuggerToolUIBase
{
    protected object script;

    public abstract void OnCreate();
    public abstract void Destroy();
    public abstract void Update();

    DebuggerToolUITabContent content;
    public GameObject Init(DebuggerToolUITabContent  contentPrefab, Transform contentTransform)
    {
        content = MonoBehaviour.Instantiate(contentPrefab, contentTransform);
        return content.gameObject;
    }

    public void GenerateFields(object obj, bool readOnly)
    {
        script = obj;
        foreach(System.Reflection.PropertyInfo property in obj.GetType().GetProperties())
        {
            foreach(System.Attribute attr in property.GetCustomAttributes(true))
            {
                if(!content.ContainField(property) && attr is DebuggerToolAccessAttribute)
                {
                    CreateField(property.GetValue(obj).GetType(), property,  obj, readOnly);
                }
            }
        }

        OnCreate();
    }

    protected void CreateField(System.Type type, System.Reflection.PropertyInfo property, object script, bool readOnly)
    {
       content.AddField(type, property, script, readOnly);
    }
}

#endif
