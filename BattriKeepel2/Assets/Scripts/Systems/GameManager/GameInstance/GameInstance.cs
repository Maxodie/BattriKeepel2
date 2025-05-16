public class GameInstanceDynamicSO : DynamicScriptableObject
{
    public SO_GameLevelData currentLevelData;
    public SO_PlayerData currentPlayerData;
}

public static class GameInstance
{
    public static SO_GameInstance m_data;
    static GameInstanceDynamicSO m_instanceData = new();

    public static void StartGameInstance(SO_GameInstance data)
    {
        m_data = data;
    }

    public static void SetCurrentBossLevel(SO_GameLevelData bossLeveldata)
    {
        m_instanceData.currentLevelData = bossLeveldata;
    }

    public static void SetCurrentPlayerData(SO_PlayerData playerData)
    {
        m_instanceData.currentPlayerData = playerData;
    }

    public static SO_GameLevelData GetCurrentLevelData()
    {
        return m_instanceData.currentLevelData;
    }

    public static SO_PlayerData GetCurrentPlayerData()
    {
        return m_instanceData.currentPlayerData;
    }
}
