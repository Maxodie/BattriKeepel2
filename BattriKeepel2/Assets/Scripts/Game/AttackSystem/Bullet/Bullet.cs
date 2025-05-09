using System;
using Components;
using Game.Entities;
using UnityEngine;

namespace Game.AttackSystem.Bullet
{
    public class Bullet : IGameEntity
    {
        private Hitbox hitbox;
        private BulletBehaviour bulletBehaviour;

        private Entity owner;

        private BulletGraphics bulletGraphics;
        public readonly BulletData data;

        public Bullet(BulletData bulletData, Transform spawnTransform)
        {
            data = bulletData;
            bulletGraphics = GraphicsManager.Get().GenerateVisualInfos<BulletGraphics>(data.BulletGraphics, spawnTransform, this, false);
            bulletGraphics.transform.position = spawnTransform.position;
            bulletGraphics.Bullet = this;

            bulletBehaviour = data.BulletBehaviour;

            owner = bulletData.Owner;

            hitbox = bulletData.BulletHitbox;
            hitbox.Init(bulletGraphics.bulletTransform);
            hitbox.BindOnCollision(OnBulletCollision);

            bulletBehaviour = data.BulletBehaviour;
        }

        private void OnBulletCollision(Hit hitCollision)
        {
            if (hitCollision.hitObject.GetComponent<PlayerGraphics>())
            {
                PlayerGraphics player = hitCollision.hitObject.GetComponent<PlayerGraphics>();
                if (owner.entityType == Entity.EntityType.Player && player.GetPlayer().entityType == Entity.EntityType.Player) return;
                
                player.GetPlayer().TakeDamage(this);
            }
        }

        public float GetSpeed()
        {
            return data.Speed;
        }

        public Entity GetOwner()
        {
            return owner;
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
        public Hitbox BulletHitbox;
        public BulletBehaviour BulletBehaviour;
        public BulletGraphics BulletGraphics;
        public float Speed;
        public float Damage;
    }
}
