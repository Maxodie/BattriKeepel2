using System.Collections;
using Game.AttackSystem.Attacks;
using Game.Entities;
using GameEntity;
using UnityEngine;

namespace Game.Managers
{
    public class AttackManager
    {
        private bool _isPlayer;
        public AttackSet attacks;
        private Entity _entityAttached;

        private MonoBehaviour coroutineLauncher;
        private Coroutine currentAttackCoroutine;

        private Awaitable currentAttack;

        private bool _isAbleToAttack = true;

        public void InitAttacking(bool isPlayer, AttackSet attackSet, Entity entityAttached, MonoBehaviour mono)
        {
            _isPlayer = isPlayer;
            
            attacks = attackSet;
            _entityAttached = entityAttached;
            coroutineLauncher = mono;

            StartAttacking();
        }

        public void StartAttacking()
        {
            currentAttackCoroutine = coroutineLauncher.StartCoroutine(DelayedAttacks(attacks.BasicAttack));
        }

        public void StartUltimate()
        {
            currentAttackCoroutine = coroutineLauncher.StartCoroutine(DelayedAttacks(attacks.UltimateAttack));
        }

        public void CancelAttack()
        {
            if (currentAttackCoroutine != null) {
                coroutineLauncher.StopCoroutine(currentAttackCoroutine);
            }
        }

        private IEnumerator DelayedAttacks(Attack attack)
        {
            yield return new WaitForSeconds(attack.BaseCooldown);
            
            if (_isPlayer && _isAbleToAttack) {
                attacks.BasicAttack.RaiseAttack((Player)_entityAttached);
            }
            
            currentAttackCoroutine = coroutineLauncher.StartCoroutine(DelayedAttacks(attack));
        }

        public void CanAttack(bool isAbleToAttack)
        {
            _isAbleToAttack = isAbleToAttack;
        }
    }
}
