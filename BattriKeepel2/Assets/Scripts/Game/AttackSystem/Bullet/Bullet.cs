using System;
using Game.Entities;
using UnityEngine;

namespace Game.AttackSystem.Bullet
{
    public class Bullet : IGameEntity
    {
        private BulletBehaviour bulletBehaviour;

        private Entity owner;

        private BulletGraphics bulletGraphics;
        public readonly BulletData data;

        public Bullet(BulletData bulletData, Transform spawnTransform)
        {
            data = bulletData;
            bulletGraphics = GraphicsManager.Get().GenerateVisualInfos<BulletGraphics>(data.BulletGraphics, spawnTransform, this, false);

            bulletBehaviour = data.BulletBehaviour;
        }

        /*private void OnBulletCollision(Hit hitCollision)*/
        /*{*/
        /*    Entity collisionEntity = hitCollision.hitObject.GetComponent<Entity>();*/
        /**/
        /*    if ((owner.entityType == Entity.EntityType.Enemy || owner.entityType == Entity.EntityType.Boss) && (collisionEntity.entityType == Entity.EntityType.Enemy || collisionEntity.entityType == Entity.EntityType.Boss)) return;*/
        /*    if (owner.entityType == Entity.EntityType.Player && collisionEntity.entityType == Entity.EntityType.Player) return;*/
        /**/
        /*    collisionEntity.TakeDamage(this);*/
        /*}*/

        public float GetSpeed()
        {
            return data.Speed;
        }

        public BulletGraphics GetBulletGraphics()
        {
            return bulletGraphics;
        }

        public BulletBehaviour GetBulletBehaviour()
        {
            return bulletBehaviour;
        }
    }

    [Serializable]
    public class BulletData
    {
        public Entity Owner;
        public BulletBehaviour BulletBehaviour;
        public BulletGraphics BulletGraphics;
        public float Speed;
        public float Damage;
    }
}
