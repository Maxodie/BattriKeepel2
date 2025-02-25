using System.Collections;
using System.Collections.Generic;
using Game.AttackSystem.Attacks;
using UnityEngine;

namespace Game.Managers
{
    public class AttackManager : MonoBehaviour
    {
        [SerializeField] private bool isPlayer;
        [SerializeField] private AttackSet attacks;

        public void InitAttacking()
        {
            if (!isPlayer) return; //Temporary : To remove when we'll code the enemies attacks

            StartCoroutine(DelayedAttacks(attacks.BasicAttack));
        }

        private Collider GetNearestTarget()
        {
            Collider nearestTarget = null;
            float minDistance = float.MaxValue;
            
            List<Collider> enemies = EnemyManager.Instance.currentEnemies;
            foreach (Collider enemy in enemies)
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
