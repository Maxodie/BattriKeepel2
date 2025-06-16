using UnityEngine;

public class BossAttack : IGameEntity {
    BossAttackGraphics m_attackGraphics;

    public BossAttack(SO_BossAttackData data, Vector2 position) {
        m_attackGraphics = GraphicsManager.Get().GenerateVisualInfos<BossAttackGraphics>(data.bossAttackGraphics, position, Quaternion.identity, this);
    }
}
