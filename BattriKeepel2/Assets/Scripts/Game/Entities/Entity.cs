using Game.AttackSystem.Attacks;
using Game.AttackSystem.Bullet;
using Game.Managers;
using UnityEngine;

namespace Game.Entities
{
    public abstract class Entity : IGameEntity
    {
        protected SO_BulletData bulletData;
        public AttackManager attackManager;

        public enum EntityType {Player, Enemy, Boss}
        public EntityType entityType;

        protected float MaxHealth;
        protected float Health;

        protected virtual void Init(AttackSet attackSet, MonoBehaviour monoBehaviour)
        {
            attackManager = new AttackManager();
            attackManager.InitAttacking(entityType == EntityType.Player, attackSet, this, monoBehaviour);
        }

        public abstract void TakeDamage(Bullet bullet);

        public virtual void HealthCheck()
        {
            if(Health <= 0)
            {
                Health =  0;
                Die();
            }
            else if(Health > MaxHealth)
            {
                Health = MaxHealth;
            }
        }

        public abstract void Die();

        protected float CalculateBaseDamages(Bullet bullet)
        {
            return bullet.data.damage;
        }

        public virtual float GetHealth()
        {
            return Health;
        }

        public virtual void CreateBullet()
        {

        }
    }
}
