using Game.AttackSystem.Attacks;
using Game.AttackSystem.Bullet;
using UnityEngine;

namespace Game.Entities
{
    public class Entity : GameEntityMonoBehaviour
    {
        [SerializeField] private AttackSet attackSet;
        [SerializeField] private BulletData bulletData;
        
        public enum EntityType {Player, Enemy, Boss}
        public EntityType entityType;

        protected float MaxHealth;
        protected float Health;

        public virtual void TakeDamage(Bullet bullet) { }

        public float GetHealth()
        {
            return Health;
        }
    }
}