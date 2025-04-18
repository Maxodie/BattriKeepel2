using System;
using Components;
using Game.Entities;
using UnityEngine;

namespace Game.AttackSystem.Bullet
{
    public class Bullet : IGameEntity
    {
        [SerializeField] private Hitbox hitbox;
        private BulletBehaviour bulletBehaviour;

        private Entity owner;
        private float speed;
        private float damage;

        private BulletGraphics bulletGraphics;

        private void InitBullet(BulletData bulletData, Transform spawnTransform)
        {
            bulletGraphics = GraphicsManager.Get().GenerateVisualInfos<BulletGraphics>(bulletData.BulletGraphics, spawnTransform, this, false);

            hitbox.Init(bulletGraphics.transform);
            hitbox.BindOnCollision(OnBulletCollision);

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

        private void OnBulletCollision(Transform transformCollision)
        {
            Entity collisionEntity = transformCollision.GetComponent<Entity>();

            if ((owner.entityType == Entity.EntityType.Enemy || owner.entityType == Entity.EntityType.Boss) && (collisionEntity.entityType == Entity.EntityType.Enemy || collisionEntity.entityType == Entity.EntityType.Boss)) return;
            if (owner.entityType == Entity.EntityType.Player && collisionEntity.entityType == Entity.EntityType.Player) return;

            collisionEntity.TakeDamage(this);
        }

        public float GetSpeed()
        {
            return speed;
        }

        public BulletGraphics GetBulletGraphics()
        {
            return bulletGraphics;
        }
    }

    [Serializable]
    public struct BulletData
    {
        public Entity Owner;
        public BulletBehaviour BulletBehaviour;
        public BulletGraphics BulletGraphics;
        public float Speed;
        public float Damage;
    }
}
