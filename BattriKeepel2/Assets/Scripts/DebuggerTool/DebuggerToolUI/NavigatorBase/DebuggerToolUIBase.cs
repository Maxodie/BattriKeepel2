public abstract class DebuggerToolUIBase
{
    public abstract void CreateUI(DebuggerToolUITabContent tabContent);

    protected void CreateField(DebuggerToolUITabContent tabContent, System.Type fieldType)
    {
       tabContent.AddField(fieldType);
    }
}
