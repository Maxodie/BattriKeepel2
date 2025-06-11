using UnityEngine;

namespace Game.AttackSystem.Bullet
{
    public class BulletGraphics : GameEntityGraphics
    {
        [SerializeField] Sprite sprite;
        [SerializeField] SpriteRenderer bulletSpriteRenderer;
        
        public Bullet Bullet;
        
        private void FixedUpdate()
        {
            if (Bullet.GetBulletBehaviour().NeedConstantUpdate) {
                Bullet.GetBulletBehaviour().RaiseBullet(Bullet);
            }
        }

        public void AutoDestroy()
        {
            Destroy(gameObject);
        }
    }
}