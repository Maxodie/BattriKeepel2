using System.Collections;
using System.Collections.Generic;
using Game.AttackSystem.Attacks;
using Game.Entities;
using UnityEngine;

namespace Game.Managers
{
    public class AttackManager : GameEntityMonoBehaviour
    {
        [SerializeField] private bool isPlayer;
        [SerializeField] private AttackSet attacks;

        public void InitAttacking()
        {
            StartCoroutine(DelayedAttacks(attacks.BasicAttack));
        }

        private Entity GetNearestTarget()
        {
            Entity nearestTarget = null;
            float minDistance = float.MaxValue;
            
            List<Entity> enemies = EnemyManager.Instance.currentEnemies;
            foreach (Entity enemy in enemies)
            {
                float distance = Vector2.Distance(transform.position, enemy.transform.position);
                if (distance > minDistance) continue;

                nearestTarget = enemy;
                minDistance = distance;
            }
            return nearestTarget;
        }

        private IEnumerator DelayedAttacks(Attack attack)
        {
            yield return new WaitForSeconds(attack.BaseCooldown);
            
            attack.RaiseAttack(GetNearestTarget());
        }
    }
}
