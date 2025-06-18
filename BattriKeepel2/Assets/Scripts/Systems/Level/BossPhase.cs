public class BossPhase : LevelPhase
{
    BossEntity m_boss;
    public override void OnStart()
    {
        SO_BossScriptableObject bossData = ((SO_BossPhase)m_levelPhase).bossData;
        m_boss = new BossEntity(bossData);
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
