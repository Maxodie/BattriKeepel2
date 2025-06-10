using System.Collections;
using System.Collections.Generic;
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
                attack.RaiseAttack((Player)_entityAttached);
            }
            
            currentAttackCoroutine = coroutineLauncher.StartCoroutine(DelayedAttacks(attacks.BasicAttack));
        }

        public void CanAttack(bool isAbleToAttack)
        {
            _isAbleToAttack = isAbleToAttack;
        }
    }
}
