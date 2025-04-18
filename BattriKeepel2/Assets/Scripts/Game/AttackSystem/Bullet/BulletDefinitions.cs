using UnityEngine;

namespace Game.AttackSystem.Bullet
{
    [CreateAssetMenu(fileName = "BulletDefinitions", menuName = "AttackSystem/Bullet/BulletDefinitions")]
    public class BulletDefinitions : ScriptableObject
    {
        public void BasicBullet(Bullet bullet)
        {
            bullet.GetBulletGraphics().transform.position += new Vector3(bullet.GetSpeed(), 0, 0);
        }
    }
}
