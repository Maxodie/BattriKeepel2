using UnityEngine;
using Game.Entities;
using Game.AttackSystem.Bullet;
using Components;

public class BossEntity : Entity
{
    SO_BossScriptableObject m_data;
    BossGraphicsEntity m_bossGraphics;

    BossMovement m_movement;

    DialogComponent m_dialogComponent;

    public BossEntity(SO_BossScriptableObject data)
    {
        m_data = data;
        MaxHealth = m_data.health;
        Health = MaxHealth;

        m_bossGraphics = GraphicsManager.Get().GenerateVisualInfos<BossGraphicsEntity>(data.bossGraphicsEntity, new Vector2(0, 2), Quaternion.identity, this);
        m_bossGraphics.ComputeLocations();

        m_movement = new BossMovement(m_bossGraphics, m_data.movementData);

        if(data.dialogData)
        {
            m_dialogComponent = new DialogComponent();
            m_dialogComponent.StartDialog(data.dialogData);
        }

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
