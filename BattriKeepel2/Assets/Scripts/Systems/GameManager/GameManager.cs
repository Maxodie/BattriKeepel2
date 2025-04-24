public class GameManagerLogger : Logger
{
}

public abstract class GameManager : GameEntityMonoBehaviour
{
    protected virtual void Awake()
    {
        OnUIManagerCreated();
    }

    protected virtual void Update()
    {

    }

    protected abstract void OnUIManagerCreated();
}
