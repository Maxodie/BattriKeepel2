using UnityEngine;
using Game.Entities;
using Game.AttackSystem.Bullet;
using Components;

public class BossEntity : Entity
{
    SO_BossScriptableObject m_data;
    BossGraphicsEntity m_bossGraphics;

    Hitbox m_hitBox;
    BossMovement m_movement;

    public BossEntity(SO_BossScriptableObject data, Transform spawnPoint)
    {
        m_data = data;

        m_bossGraphics = GraphicsManager.Get().GenerateVisualInfos<BossGraphicsEntity>(data.bossGraphicsEntity, spawnPoint, this);
        m_movement = new BossMovement(m_bossGraphics, m_data.movementData);

        m_hitBox = m_data.hitbox;
        m_hitBox.Init(m_bossGraphics.transform);

        UpdateVisualHealth();
    }

    public void Update()
    {
        m_movement.Update();
    }

    public override void TakeDamage(Bullet bullet)
    {
        Health -=  CalculateBaseDamages(bullet);
        HealthCheck();

        UpdateVisualHealth();
    }

    void UpdateVisualHealth()
    {
        m_bossGraphics.SetHP(Health / MaxHealth);
    }

    public override void Die()
    {

    }
}
