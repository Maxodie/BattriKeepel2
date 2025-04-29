public class GameInstanceDynamicSO : DynamicScriptableObject
{
    public SO_BossLevelData currentBossLevelData;
}

public static class GameInstance
{
    public static SO_GameInstance m_data;
    static GameInstanceDynamicSO m_instanceData;

    public static void StartGameInstance(SO_GameInstance data)
    {
        m_data = data;
    }

    public static void SetCurrentBossLevel(SO_BossLevelData bossLeveldata)
    {
        m_instanceData.currentBossLevelData = bossLeveldata;
    }

    public static SO_BossLevelData GetCurrentBossLevel()
    {
        return m_instanceData.currentBossLevelData;
    }
}
