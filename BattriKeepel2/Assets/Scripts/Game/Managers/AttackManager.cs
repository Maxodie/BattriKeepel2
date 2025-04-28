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
        private bool _isAbleToAttack;

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

        private Entity GetNearestTarget()
        {
            Entity nearestTarget = null;
            float minDistance = float.MaxValue;

            List<Entity> enemies = EnemyManager.Instance.currentEnemies;
            foreach (Entity enemy in enemies)
            {
//              float distance = Vector2.Distance(transform.position, enemy.GetEntityGraphics().transform.position); // ça existe pas ta merde là jose nique bien tes morts
                //if (distance > minDistance) continue;

                nearestTarget = enemy;
                //minDistance = distance;
            }
            return nearestTarget;
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
                
                attack.RaiseAttack(GetNearestTarget());
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
