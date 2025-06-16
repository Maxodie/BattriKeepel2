using UnityEngine;

public class Bullet : IGameEntity
{
    private BulletGraphics bulletGraphics;
    private Vector2 m_direction;
    public SO_BulletData data;
    private Vector3 position;
    private bool m_isDead = false;

    public Bullet(SO_BulletData data, Vector3 position, Transform spawnTransform, bool child)
    {
        this.data = data;
        this.position = position;
        bulletGraphics = data.bulletGraphics;
        bulletGraphics = GraphicsManager.Get()
            .GenerateVisualInfos<BulletGraphics>(bulletGraphics, spawnTransform, this, child);
        bulletGraphics.transform.position = this.position;
        bulletGraphics.transform.LookAt(spawnTransform);
    }

    public void Update() {
        Vector2 m_vel = -bulletGraphics.transform.forward * data.speed * Time.deltaTime;
        bulletGraphics.transform.position += new Vector3(m_vel.x, m_vel.y, 0);
        CheckForDeath();
        Log.Info();
    }

    public bool IsDead() {
        return m_isDead;
    }

    private void CheckForDeath() {
        if (Vector2.Distance(bulletGraphics.transform.position, position) > Camera.main.scaledPixelHeight * .03f) {
            MonoBehaviour.Destroy(bulletGraphics.gameObject);
            m_isDead = true;
        }
    }

    public BulletGraphics GetGraphics() {
        return this.bulletGraphics;
    }
}
