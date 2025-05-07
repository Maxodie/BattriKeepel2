using Game.AttackSystem.Attacks;
using Game.AttackSystem.Bullet;

namespace Game.Entities
{
    public class Enemy : Entity
    {
        protected override void Init(AttackSet attackSet)
        {
            base.Init(null);
        }

        public override void TakeDamage(Bullet bullet)
        {

        }

        public override void Die()
        {

        }
    }
}
