public abstract class DebuggerToolBase
{
    public abstract void Create();
    public abstract void Destroy();
    public abstract void Update();

    public bool IsToolActive {set; get;}
}
