using Game.AttackSystem.Attacks;
using Game.AttackSystem.Bullet;
using Game.Managers;

namespace Game.Entities
{
    public abstract class Entity : IGameEntity
    {
        protected BulletData bulletData;
        private EntityGraphics entityGraphics;
        public AttackManager attackManager;

        public enum EntityType {Player, Enemy, Boss}
        public EntityType entityType;

        protected float MaxHealth;
        protected float Health;

        protected virtual void Init(AttackSet attackSet)
        {
            attackManager = new AttackManager();
            attackManager.InitAttacking(entityType == EntityType.Player, attackSet, this);
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
            return bullet.data.Damage;
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
