public abstract class DebuggerToolUIBase
{
    private void GenerateFields(DebuggerToolUITabContent tabContent, object obj)
    {

        /*foreach (System.Attribute attr in attrs)*/
        /*{*/
        /*    Log.Info<DebuggerLogger>("attr : " + attr.ToString());*/
        /*    if (attr is DebuggerToolAccessAttribute)*/
        /*    {*/
        /*        foreach(System.Reflection.PropertyInfo property in attr.GetType().GetProperties())*/
        /*        {*/
        /*            Log.Info<DebuggerLogger>("new filed : " + property.Name + " | " + obj.ToString());*/
        /*            CreateField(tabContent, property, obj);*/
        /*        }*/
        /*    }*/
        /*}*/
        foreach(System.Reflection.PropertyInfo property in obj.GetType().GetProperties())
        {
            foreach(System.Attribute attr in property.GetCustomAttributes(true))
            {
                if(attr is DebuggerToolAccessAttribute)
                {
                    Log.Info<DebuggerLogger>("new filed : " + property.Name + " | " + obj.ToString());
                    CreateField(tabContent, property.GetValue(obj).GetType(), property,  obj);
                }
            }
        }
    }

    public void CreateUI(DebuggerToolUITabContent tabContent, object script)
    {
        GenerateFields(tabContent, script);
    }

    protected void CreateField(DebuggerToolUITabContent tabContent,System.Type type, System.Reflection.PropertyInfo property, object script)
    {
       tabContent.AddField(type, property, script);
    }
}
