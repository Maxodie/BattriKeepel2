using Game.AttackSystem.Attacks;
using Game.AttackSystem.Bullet;
using UnityEngine;

namespace Game.Entities
{
    public class Enemy : Entity
    {
        protected override void Init(AttackSet attackSet, MonoBehaviour monoBehaviour)
        {
            base.Init(null, monoBehaviour);
        }

        public override void TakeDamage(Bullet bullet)
        {

        }

        public override void Die()
        {

        }
    }
}
