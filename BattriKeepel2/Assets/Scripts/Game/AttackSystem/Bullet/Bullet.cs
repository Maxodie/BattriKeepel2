using UnityEngine;

public class Bullet : IGameEntity
{
    private BulletGraphics bulletGraphics;
    private Vector2 m_direction;
    public SO_BulletData data;
    private Vector3 position;
    private bool m_isDead = false;

    float maxDistance;

    public Bullet(AttackGraphicsPool pool, SO_BulletData data, Vector3 position, Transform spawnTransform, bool child, Vector3 up, System.Type damageable)
    {
        this.data = data;
        this.position = position;
        bulletGraphics = pool.GetBulletGraphics(spawnTransform, this, child);
        bulletGraphics.transform.position = this.position;
        bulletGraphics.SetPool(pool);
        bulletGraphics.Setup(damageable, data, this);

        bulletGraphics.transform.rotation = Quaternion.LookRotation(Vector3.forward, up);

        maxDistance = GraphicsManager.Get().BoundsMax(Camera.main).magnitude * 2;
    }

    public void Update() {
        Vector2 m_vel = bulletGraphics.transform.up * data.speed * Time.deltaTime;
        bulletGraphics.transform.position += new Vector3(m_vel.x, m_vel.y, 0);
        CheckForDeath();
    }

    public bool IsDead() {
        return m_isDead;
    }

    public void Kill()
    {
        if(!m_isDead)
        {
            m_isDead = true;
            bulletGraphics.DetachFromEntity();
            bulletGraphics = null;
        }
    }

    private void CheckForDeath() {
        if (Vector2.Distance(bulletGraphics.transform.position, position) > maxDistance) {
            /*Kill();*/
        }
    }

    public BulletGraphics GetGraphics() {
        return this.bulletGraphics;
    }
}
