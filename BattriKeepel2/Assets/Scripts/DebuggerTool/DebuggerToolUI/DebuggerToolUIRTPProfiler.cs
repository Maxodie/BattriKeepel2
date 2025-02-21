public class DebuggertoolUIRTProfiler : DebuggerToolUIBase
{
    public override void CreateUI(DebuggerToolUITabContent tabContent)
    {
        CreateField(tabContent, typeof(DebuggerToolUIFieldToggle));
    }
}
