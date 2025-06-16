using UnityEngine;

public class BossAttack : IGameEntity {
    BossAttackParent m_attackGraphics;

    public BossAttack(SO_BossAttackData data, Vector2 position) {
        m_attackGraphics = new BossAttackParent();
    }
}
