public class BossPhase : LevelPhase
{
    BossEntity m_boss;
    public override void OnStart(LevelManager levelManager)
    {
        SO_BossScriptableObject bossData = ((SO_BossPhase)m_levelPhase).bossData;
        m_boss = new BossEntity(levelManager.m_bulletPool, bossData, levelManager.m_player);
    }

    public override void OnEnd()
    {
        m_boss.Destroy();
    }

    public override void Update()
    {
        m_boss.Update();
        if(m_boss.IsDead())
        {
            EndPhase();
        }
    }
}
