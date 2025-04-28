public class DebuggerToolUIFieldInt : DebuggerToolUIFieldDouble
{
    public override void UpdateProperty(string value)
    {
        SetValue(System.Int32.Parse(value));
    }
}
