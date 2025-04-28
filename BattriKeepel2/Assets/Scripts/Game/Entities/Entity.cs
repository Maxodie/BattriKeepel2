using Game.AttackSystem.Attacks;
using Game.AttackSystem.Bullet;
using Game.Managers;

namespace Game.Entities
{
    public abstract class Entity : IGameEntity
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

        public abstract void TakeDamage(Bullet bullet);

        public virtual float GetHealth()
        {
            return Health;
        }
    }
}
