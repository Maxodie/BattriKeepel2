public abstract class DebuggerToolUIBase
{
    private static void GenerateFields(ref object obj)
    {
        System.Attribute[] attrs = System.Attribute.GetCustomAttributes(obj.GetType());

        foreach (System.Attribute attr in attrs)
        {
            if (attr is DebuggerToolAccessAttribute a)
            {
                foreach(System.Reflection.PropertyInfo property in a.GetType().GetProperties())
                {
                    //CreateField(ref property.GetValue(obj));
                }
            }
        }
    }

    public void CreateUI(DebuggerToolUITabContent tabContent, object script)
    {
        GenerateFields(ref script);
    }

    protected void CreateField(DebuggerToolUITabContent tabContent, System.Type fieldType, ref object value)
    {
       tabContent.AddField(fieldType, ref value);
    }
}
