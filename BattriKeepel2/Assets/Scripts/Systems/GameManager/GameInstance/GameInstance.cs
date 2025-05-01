public class GameInstanceDynamicSO : DynamicScriptableObject
{
    public SO_GameLevelData currentBossLevelData;
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
        m_instanceData.currentBossLevelData = bossLeveldata;
    }

    public static void SetCurrentPlayerLevel(SO_PlayerData bossData)
    {
        m_instanceData.currentPlayerData = bossData;
    }

    public static SO_GameLevelData GetCurrentBossLevel()
    {
        return m_instanceData.currentBossLevelData;
    }

    public static SO_PlayerData GetCurrentPlayerData()
    {
        return m_instanceData.currentPlayerData;
    }
}
