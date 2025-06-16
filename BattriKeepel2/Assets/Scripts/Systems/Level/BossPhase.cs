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
    }

    public override void Update()
    {
        m_boss.Update();
    }
}
