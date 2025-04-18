using Game.AttackSystem.Attacks;
using Game.AttackSystem.Bullet;
using UnityEngine;

namespace Game.Entities
{
    public class Entity : IGameEntity
    {
        [SerializeField] private AttackSet attackSet;
        [SerializeField] private BulletData bulletData;
        [SerializeField] private EntityGraphics entityGraphics;
        
        public enum EntityType {Player, Enemy, Boss}
        public EntityType entityType;

        protected float MaxHealth;
        protected float Health;

        public virtual void TakeDamage(Bullet bullet) { }

        public float GetHealth()
        {
            return Health;
        }

        public EntityGraphics GetEntityGraphics()
        {
            return entityGraphics;
        }
    }
}