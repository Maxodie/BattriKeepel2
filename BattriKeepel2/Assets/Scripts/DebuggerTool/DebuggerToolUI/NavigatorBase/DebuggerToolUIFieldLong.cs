public class DebuggerToolUIFieldLong : DebuggerToolUIFieldDouble
{
    public override void UpdateProperty(string value)
    {
        SetValue(System.Int64.Parse(value));
    }
}
