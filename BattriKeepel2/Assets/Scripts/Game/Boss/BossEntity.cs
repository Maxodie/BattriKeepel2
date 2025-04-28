using UnityEngine;
using Game.Entities;

public class BossEntity : Entity
{
    SO_BossGraphicsScriptableObject m_data;
    BossGraphicsEntity m_bossGraphics;

    public BossEntity(SO_BossGraphicsScriptableObject data, Transform spawnPoint)
    {
        m_data = data;

        m_bossGraphics = GraphicsManager.Get().GenerateVisualInfos<BossGraphicsEntity>(data.bossGraphicsEntity, spawnPoint, this);
    }
}
