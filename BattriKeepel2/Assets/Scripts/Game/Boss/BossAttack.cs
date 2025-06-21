using System;

public class BossAttack : IGameEntity {
    public BossAttackParent m_attackGraphics;
    public bool isActive;

    public BossAttack(BossEntity boss, AttackGraphicsPool attackPool, SO_BossAttackData attackData, GameEntity.Player player)
    {
        isActive = true;
        m_attackGraphics = Activator.CreateInstance(attackData.GetAttackType()) as BossAttackParent;
        m_attackGraphics.Init(boss, attackData, attackPool, player);
    }

    public void Update()
    {
        m_attackGraphics.Update();
    }

    public void Clean()
    {
        if(isActive)
        {
            m_attackGraphics.Clean();
            m_attackGraphics = null;
            isActive = false;
        }
    }

}
