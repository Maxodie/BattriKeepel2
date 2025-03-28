using UnityEngine;

public abstract class DebuggerToolUIBase
{
    DebuggerToolUITabContent content;
    public GameObject Init(DebuggerToolUITabContent  contentPrefab, Transform contentTransform)
    {
        content = MonoBehaviour.Instantiate(contentPrefab, contentTransform);
        return content.gameObject;
    }

    public void GenerateFields(object obj)
    {
        foreach(System.Reflection.PropertyInfo property in obj.GetType().GetProperties())
        {
            foreach(System.Attribute attr in property.GetCustomAttributes(true))
            {
                if(!content.ContainField(property) && attr is DebuggerToolAccessAttribute)
                {
                    CreateField(property.GetValue(obj).GetType(), property,  obj);
                }
            }
        }
    }

    protected void CreateField(System.Type type, System.Reflection.PropertyInfo property, object script)
    {
       content.AddField(type, property, script);
    }
}
