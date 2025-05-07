using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Game.AttackSystem.Attacks;
using Game.Entities;

namespace Game.Managers
{
    public class AttackManager
    {
        private bool _isPlayer;
        private AttackSet _attacks;
        private Entity _entityAttached;
        
        private CancellationTokenSource _cancellationTokenSource;
        private bool _isAbleToAttack = true;

        public void InitAttacking(bool isPlayer, AttackSet attackSet, Entity entityAttached)
        {
            _isPlayer = isPlayer;
            _attacks = attackSet;
            _entityAttached = entityAttached;
            
            StartAttacking();
        }
        
        private void StartAttacking()
        {
            DelayedAttacks(_attacks.BasicAttack);
        }
        
        private async void DelayedAttacks(Attack attack)
        {
            _cancellationTokenSource = new CancellationTokenSource();

            try
            {
                await Task.Delay(attack.BaseCooldown * 1000, _cancellationTokenSource.Token);
                _cancellationTokenSource.Dispose();
                _cancellationTokenSource = null;

                if (!_isPlayer || !_isAbleToAttack) return;
                
                attack.RaiseAttack();
                DelayedAttacks(attack);
            }
            catch
            {
                _cancellationTokenSource?.Dispose();
                _cancellationTokenSource = null;
            }
        }
    }
}
