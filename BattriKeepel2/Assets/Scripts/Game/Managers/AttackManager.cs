using Game.AttackSystem.Attacks;
using Game.Entities;
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

        private void StartAttacking()
        {
            currentAttack = DelayedAttacks(attacks.BasicAttack);
        }

        private void CancelAttack()
        {
            if (currentAttack != null && !currentAttack.IsCompleted) {
                currentAttack.Cancel();
            }
        }

        private async Awaitable DelayedAttacks(Attack attack)
        {
            if (_isPlayer && _isAbleToAttack) {
                attack.RaiseAttack(_entityAttached);
            }

            await Awaitable.WaitForSecondsAsync(attack.BaseCooldown);

            currentAttack = DelayedAttacks(attack);
        }
    }
}
