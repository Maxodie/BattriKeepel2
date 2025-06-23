using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BulletGraphics : GameEntityGraphics
{
    [SerializeField] public SpriteRenderer sprite;
    [SerializeField] public Light2D lights;
    [SerializeField] public ParticleSystem particles;
    SO_BulletData data;
    [SerializeField] public TrailRenderer trail;
    [SerializeField] public Collider2D collide2D;

    System.Type m_damageableType;
    Bullet linkdeBulled;

    AttackGraphicsPool m_pool;
    public void SetPool(AttackGraphicsPool pool)
    {
        m_pool = pool;
    }

    public void StartPool()
    {
        trail.enabled = true;
        gameObject.SetActive(true);
        sprite.gameObject.SetActive(true);
        lights.gameObject.SetActive(true);
        collide2D.enabled = true;
    }

    public void EndPool()
    {
        collide2D.enabled = false;
        transform.SetParent(null);
        gameObject.SetActive(false);
    }

    public void DetachFromEntity()
    {
        StartCoroutine(StartParticles());
    }

    public void Setup(System.Type damageable, SO_BulletData bulletData, Bullet bullet)
    {
        m_damageableType = damageable;
        data = bulletData;
        linkdeBulled = bullet;

        sprite.color = bulletData.color;
        lights.color = bulletData.color;
        trail.startColor = bulletData.color;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        EntityGraphics entityGraphics = col.gameObject.GetComponent<EntityGraphics>();
        if(entityGraphics && entityGraphics.GetOwner().GetType() == m_damageableType)
        {
            entityGraphics.TakeDamage(data.damage);

            linkdeBulled.Kill();
        }
    }

    private IEnumerator StartParticles()
    {
        trail.enabled = false;

        sprite.gameObject.SetActive(false);
        lights.gameObject.SetActive(false);
        particles.gameObject.SetActive(true);
        collide2D.enabled = false;
        particles.Play();

        yield return new WaitForSeconds(0.5f);
        m_pool.StopBullet(this);
    }
}
