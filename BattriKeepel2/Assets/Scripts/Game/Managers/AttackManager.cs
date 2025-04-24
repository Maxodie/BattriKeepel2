using System.Collections.Generic;
using Game.AttackSystem.Attacks;
using Game.Entities;
using UnityEngine;

namespace Game.Managers
{
    public class AttackManager
    {
        private bool _isPlayer;
        private AttackSet _attacks;
        private Entity _entityAttached;

        public void Init(bool isPlayer, AttackSet attackSet, Entity entityAttached)
        {
            _isPlayer = isPlayer;
            _attacks = attackSet;
            _entityAttached = entityAttached;
            
            StartAttacking();
        }
        
        private void StartAttacking()
        {
            DelayAttacks(_attacks.BasicAttack);
        }

        private Entity GetNearestTarget()
        {
            Entity nearestTarget = null;
            float minDistance = float.MaxValue;
            
            List<Entity> enemies = EnemyManager.Instance.currentEnemies;
            foreach (Entity enemy in enemies)
            {
                float distance = Vector2.Distance( _entityAttached.GetEntityGraphics().transform.position, enemy.GetEntityGraphics().transform.position);
                if (distance > minDistance) continue;

                nearestTarget = enemy;
                minDistance = distance;
            }
            return nearestTarget;
        }

        private async void DelayAttacks(Attack attack)
        {
            float cooldown = Time.deltaTime;
            
            while (Time.deltaTime - cooldown > attack.BaseCooldown) {
                await Awaitable.NextFrameAsync();
            }
            
            attack.RaiseAttack(GetNearestTarget());
            DelayAttacks(attack);
        }
    }
}
