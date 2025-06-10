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

        private Awaitable currentAttack;

        private bool _isAbleToAttack = true;

        public void InitAttacking(bool isPlayer, AttackSet attackSet, Entity entityAttached)
        {
            _isPlayer = isPlayer;
            attacks = attackSet;
            _entityAttached = entityAttached;

            StartAttacking();
        }

        public void StartAttacking()
        {
            currentAttack = DelayedAttacks(attacks.BasicAttack);
        }

        public void CancelAttack()
        {
            if (currentAttack != null) {
                currentAttack.Cancel();
            }
        }

        private async Awaitable DelayedAttacks(Attack attack)
        {
            await Awaitable.WaitForSecondsAsync(attack.BaseCooldown);
            
            if (_isPlayer && _isAbleToAttack) {
                attack.RaiseAttack((Player)_entityAttached);
            }
            
            currentAttack = DelayedAttacks(attack);
        }

        public void CanAttack(bool isAbleToAttack)
        {
            _isAbleToAttack = isAbleToAttack;
        }
    }
}
