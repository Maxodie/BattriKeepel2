using UnityEngine;
using UnityEngine.UI;

namespace Game.AttackSystem.Bullet
{
    public class BulletGraphics : GameEntityGraphics
    {
        [SerializeField] private Image sprite;
        [SerializeField] private Bullet bullet;
        
        private void FixedUpdate()
        {
            if (bullet.GetBulletBehaviour().NeedConstantUpdate) {
                bullet.GetBulletBehaviour().RaiseBullet(bullet);
            }
        }
    }
}