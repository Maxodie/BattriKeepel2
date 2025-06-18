using UnityEngine;
using Game.Entities;

public class BossEntity : Entity
{
    SO_BossScriptableObject m_data;
    BossGraphicsEntity m_bossGraphics;

    BossAttack[] m_attacks;

    SoundInstance soundInstance;
    bool m_isDead = false;

    BossNuisance m_nuisance;

    public BossEntity(SO_BossScriptableObject data)
    {
        m_data = data;
        MaxHealth = m_data.health;
        Health = MaxHealth;

        m_nuisance = new(data);

        m_bossGraphics = GraphicsManager.Get().GenerateVisualInfos<BossGraphicsEntity>(data.bossGraphicsEntity, new Vector2(0, 2), Quaternion.identity, this);
        m_bossGraphics.ComputeLocations();

        soundInstance = AudioManager.CreateSoundInstance(false, false);

        InitAttacks();
        HandleAttacks();

        UpdateVisualHealth();
    }

    public void Update()
    {
    }

    private void InitAttacks() {
        m_attacks = new BossAttack[m_data.attackData.Length];
    }

    private async Awaitable HandleAttacks() {
        for (int i = 0; i < m_attacks.Length; i++) {
            m_attacks[i] = new(m_data.attackData[i], m_bossGraphics.transform.position);
            await Awaitable.WaitForSecondsAsync(m_data.attackData[i].intervalBeforeNextAttack);
        }
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

        HandleAttacks().Cancel();
        m_nuisance.OnBossSetActive(false);
        Destroy();
    }

    public void Destroy()
    {
        AudioManager.DestroySoundInstance(soundInstance);
        Object.Destroy(m_bossGraphics);
    }
}
