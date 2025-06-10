using UnityEngine;

namespace Game.AttackSystem.Bullet
{
    public class Bullet : IGameEntity
    {
        private BulletGraphics bulletGraphics;
        private Vector2 m_direction;
        public SO_BulletData data;

        public Bullet(SO_BulletData data, Vector3 position, Transform spawnTransform)
        {
            this.data = data;
            bulletGraphics = data.bulletGraphics;
            bulletGraphics = GraphicsManager.Get()
                .GenerateVisualInfos<BulletGraphics>(bulletGraphics, spawnTransform, this, true);
            bulletGraphics.transform.position = position;
            bulletGraphics.transform.LookAt(spawnTransform);
        }

        public void Update() {
            Vector2 m_vel = -bulletGraphics.transform.forward * data.speed * Time.deltaTime;
            bulletGraphics.transform.position += new Vector3(m_vel.x, m_vel.y, 0);
        }

        public BulletGraphics GetGraphics() {
            return this.bulletGraphics;
        }
    }
}
