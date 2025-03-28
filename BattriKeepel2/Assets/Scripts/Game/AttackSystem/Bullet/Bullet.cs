using System;
using Game.Entities;

namespace Game.AttackSystem.Bullet
{
    public class Bullet : GameEntityMonoBehaviour
    {
        private CollisionManager collisionManager;
        private BulletBehaviour bulletBehaviour;

        private Entity owner;
        private float speed;
        private float damage;
    
        private void Awake()
        {
            collisionManager = GetComponent<CollisionManager>();
        }

        private void InitBullet(BulletData bulletData)
        {
            bulletBehaviour = bulletData.BulletBehaviour;
            speed = bulletData.Speed;
            damage = bulletData.Damage;
        }

        private void FixedUpdate()
        {
            if (bulletBehaviour.NeedConstantUpdate) {
                bulletBehaviour.RaiseBullet(this);
            }
        }

        private void DamageEntity(Entity entity)
        {
            if ((owner.entityType == Entity.EntityType.Enemy || owner.entityType == Entity.EntityType.Boss) && (entity.entityType == Entity.EntityType.Enemy || entity.entityType == Entity.EntityType.Boss)) return;
            if (owner.entityType == Entity.EntityType.Player && entity.entityType == Entity.EntityType.Player) return;
            
            entity.TakeDamage(this);
        }

        public float GetSpeed()
        {
            return speed;
        }
    }

    [Serializable]
    public struct BulletData
    {
        public Entity Owner;
        public BulletBehaviour BulletBehaviour;
        public float Speed;
        public float Damage;
    }
}
