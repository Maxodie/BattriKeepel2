using Game.AttackSystem.Attacks;
using Game.AttackSystem.Bullet;
using Game.Managers;
using UnityEngine;

namespace Game.Entities
{
    public class Entity : IGameEntity
    {
        private AttackSet attackSet;
        private BulletData bulletData;
        private EntityGraphics entityGraphics;
        private AttackManager attackManager;

        public enum EntityType {Player, Enemy, Boss}
        public EntityType entityType;

        protected float MaxHealth;
        protected float Health;
        
        protected virtual void Init()
        {
            attackManager = new AttackManager();
            attackManager.InitAttacking(entityType == EntityType.Player, attackSet, this);
        }

        public virtual void TakeDamage(Bullet bullet) { }

        public float GetHealth()
        {
            return Health;
        }
    }
}
