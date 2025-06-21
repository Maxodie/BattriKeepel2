using System;

public class BossAttack : IGameEntity {
    public BossAttackParent m_attackGraphics;

    public BossAttack(AttackGraphicsPool attackPool, SO_BossAttackData attackData, GameEntity.Player player)
    {
        m_attackGraphics = Activator.CreateInstance(attackData.GetAttackType()) as BossAttackParent;
        m_attackGraphics.Init(attackData, attackPool, player);
    }

    public void Update()
    {
        m_attackGraphics.Update();
    }

    public void Clean()
    {
        m_attackGraphics.Clean();
    }

}
