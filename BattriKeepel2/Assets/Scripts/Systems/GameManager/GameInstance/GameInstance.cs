public class GameInstanceDynamicSO : DynamicScriptableObject
{

}

public static class GameInstance
{
    public static SO_GameInstance m_data;
    static GameInstanceDynamicSO m_instanceData;

    public static void StartGameInstance(SO_GameInstance data)
    {
        m_data = data;
    }
}
