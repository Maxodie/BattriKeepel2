using UnityEngine;

namespace Game.AttackSystem.Bullet
{
    public class Bullet : MonoBehaviour
    {
        private CollisionManager _collisionManager;
        
        private BulletBehaviour _bulletBehaviour;
        
        private float _speed;
        private float _damage;
    
        private void Awake()
        {
            _collisionManager = GetComponent<CollisionManager>();
        }

        private void InitBullet(BulletData bulletData)
        {
            _bulletBehaviour = bulletData.BulletBehaviour;
            _speed = bulletData.Speed;
            _damage = bulletData.Damage;
        }

        private void FixedUpdate()
        {
            if (_bulletBehaviour.NeedConstantUpdate)
            {
                _bulletBehaviour.RaiseBullet(this);
            }
        }

        private void TakeDamage(Collider entity) //Replace "Collider" with "Entity"
        {
            if (entity.transform.CompareTag("Player"))
            {
                //Code for player damage
            }
            else if (entity.transform.CompareTag("Enemy"))
            {
                //Code for enemy damage
            }
        }
    }

    public struct BulletData
    {
        public BulletBehaviour BulletBehaviour;
        public float Speed;
        public float Damage;
    }
}
