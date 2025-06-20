using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BulletGraphics : GameEntityGraphics
{
    [SerializeField] public SpriteRenderer sprite;
    [SerializeField] public Light2D lights;
    [SerializeField] public ParticleSystem particles;
    [SerializeField] public SO_BulletData data;
    [SerializeField] public TrailRenderer trail;

    System.Type m_damageableType;

    public void Setup(System.Type damageable, SO_BulletData bulletData)
    {
        m_damageableType = damageable;
        data = bulletData;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        EntityGraphics entityGraphics = col.gameObject.GetComponent<EntityGraphics>();
        if(entityGraphics && entityGraphics.GetOwner().GetType() == m_damageableType)
        {
            entityGraphics.TakeDamage(data.damage);
            
            StartCoroutine(StartParticles());
            GetOwner<Bullet>().Kill();
        }
    }

    private IEnumerator StartParticles()
    {
        trail.enabled = false;
            
        sprite.gameObject.SetActive(false);
        lights.gameObject.SetActive(false);
        particles.gameObject.SetActive(true);
        particles.Play();
        
        yield return new WaitForSeconds(0.5f);
        
        ((Bullet)GetOwner()).Kill();
    }
}
