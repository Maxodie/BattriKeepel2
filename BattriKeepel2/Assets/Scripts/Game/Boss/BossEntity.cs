using UnityEngine;
using Game.Entities;

public class BossEntity : Entity
{
    SO_BossScriptableObject m_data;
    BossGraphicsEntity m_bossGraphics;

    BossAttackPhaseSystem m_attack;

    SoundInstance soundInstance;
    bool m_isDead = false;

    BossNuisance m_nuisance;
    AttackGraphicsPool m_bulletPool;
    GameEntity.Player m_player;

    public BossEntity(AttackGraphicsPool bulletPool, SO_BossScriptableObject data, GameEntity.Player player)
    {
        m_player = player;
        m_data = data;
        MaxHealth = m_data.health;
        Health = MaxHealth;

        m_bulletPool = bulletPool;

        m_nuisance = new(data);

        m_bossGraphics = GraphicsManager.Get().GenerateVisualInfos<BossGraphicsEntity>(data.bossGraphicsEntity, new Vector2(0, 2), Quaternion.identity, this);
        m_bossGraphics.ComputeLocations();

        soundInstance = AudioManager.CreateSoundInstance(false, false);

        InitAttacks();

        UpdateVisualHealth();
    }

    public void Update()
    {
        m_attack.UpdatePhase();
    }

    private void InitAttacks() {
        m_attack = new(m_data.attackDataPhases, m_bulletPool, m_player);
        m_attack.StartPhaseSystem();
    }

    public bool IsDead()
    {
        return m_isDead;
    }

    public override void TakeDamage(float amount)
    {
        if(IsDead())
        {
            return;
        }
        soundInstance.PlaySound(m_data.damageSound);
        Health -= amount;
        HealthCheck();

        UpdateVisualHealth();

        m_nuisance.OnTakeDammage();
    }

    void UpdateVisualHealth()
    {
        m_bossGraphics.SetHP(Health / MaxHealth);
    }

    public override void Die()
    {
        m_isDead = true;

        m_nuisance.OnBossSetActive(false);
        Destroy();
    }

    public void Destroy()
    {
        m_attack.Clear();
        AudioManager.DestroySoundInstance(soundInstance);
        Object.Destroy(m_bossGraphics);
    }
}
