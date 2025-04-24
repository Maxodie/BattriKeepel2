using Game.AttackSystem.Attacks;
using Game.AttackSystem.Bullet;
using Game.Managers;

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

        public virtual void TakeDamage(Bullet bullet) { }

        protected virtual void Init()
        {
            attackManager = new AttackManager();
            attackManager.Init(entityType == EntityType.Player, attackSet, this);
        }
        
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