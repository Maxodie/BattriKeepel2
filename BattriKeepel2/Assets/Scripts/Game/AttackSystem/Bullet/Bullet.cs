using Game.AttackSystem.Attacks;
using UnityEngine;

public class Bullet : IGameEntity
{
    private BulletGraphics bulletGraphics;
    private Vector2 m_direction;
    public SO_BulletData data;
    private Vector3 position;
    private bool m_isDead = false;

    private float _damage;
    private float _speed;

    float maxDistance;

    public Bullet(SO_BulletData data, Vector3 position, Transform spawnTransform, bool child, Vector3 up, System.Type damageable, Attack attack)
    {
        this.data = data;
        this.position = position;
        bulletGraphics = data.bulletGraphics;
        bulletGraphics = GraphicsManager.Get()
            .GenerateVisualInfos<BulletGraphics>(bulletGraphics, spawnTransform, this, child);
        bulletGraphics.transform.position = this.position;
        bulletGraphics.Setup(damageable);

        bulletGraphics.transform.rotation = Quaternion.LookRotation(Vector3.forward, up);

        _damage = attack != null ? attack.BaseDamage : 1;
        _speed = attack != null ? attack.BaseSpeed : 1;

        bulletGraphics.Damage = _damage;

        maxDistance = GraphicsManager.Get().BoundsMax(Camera.main).magnitude * 2;
    }

    public void Update() {
        Vector2 m_vel = bulletGraphics.transform.up * _speed * Time.deltaTime;
        bulletGraphics.transform.position += new Vector3(m_vel.x, m_vel.y, 0);
        CheckForDeath();
    }

    public bool IsDead() {
        return m_isDead;
    }

    public void Kill()
    {
        m_isDead = true;
        Object.Destroy(bulletGraphics.gameObject);
    }

    private void CheckForDeath() {
        if (Vector2.Distance(bulletGraphics.transform.position, position) > maxDistance) {
            MonoBehaviour.Destroy(bulletGraphics.gameObject);
            Kill();
        }
    }

    public float GetDamage()
    {
        return _damage;
    }

    public BulletGraphics GetGraphics() {
        return this.bulletGraphics;
    }
}
