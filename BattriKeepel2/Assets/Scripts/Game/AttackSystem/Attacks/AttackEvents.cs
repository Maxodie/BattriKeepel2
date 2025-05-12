using System;
using GameEntity;
using UnityEngine.Events;

namespace Game.AttackSystem.Attacks
{
    public struct AttackEvents
    {
        [Serializable]
        public class BasePlayerAttack
        {
            public UnityEvent<Player> basePlayerAttackEvent;
        }
        
        public class BaseAttack
        {
            public UnityEvent baseAttackEvent;
        }
    }
}
