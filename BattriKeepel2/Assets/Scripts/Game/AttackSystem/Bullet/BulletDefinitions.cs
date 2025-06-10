using UnityEngine;

namespace Game.AttackSystem.Bullet
{
    [CreateAssetMenu(fileName = "BulletDefinitions", menuName = "AttackSystem/Bullet/BulletDefinitions")]
    public class BulletDefinitions : ScriptableObject
    {
        public void BasicBullet(Bullet bullet)
        {
            //bullet.GetBulletGraphics().transform.position += new Vector3(0, bullet.GetSpeed(), 0);
        }
    }
}
