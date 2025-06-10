using UnityEngine;

namespace Game.AttackSystem.Bullet
{
    public class Bullet : IGameEntity
    {
        private BulletGraphics bulletGraphics;
        private Vector2 m_direction;
        public SO_BulletData data;

        public Bullet(SO_BulletData data, Vector3 position, Vector2 direction)
        {
            m_direction = direction;
            bulletGraphics = data.bulletGraphics;
            bulletGraphics = GraphicsManager.Get()
                .GenerateVisualInfos<BulletGraphics>(bulletGraphics, position, Quaternion.identity, this, false);
        }

        public void Update() {
            Vector2 m_vel = m_direction * data.speed * Time.deltaTime;
            bulletGraphics.transform.position += new Vector3(m_vel.x, m_vel.y, 0);
        }
    }
}
