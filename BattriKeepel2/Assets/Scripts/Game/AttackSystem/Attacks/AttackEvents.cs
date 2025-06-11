using System;
using UnityEngine.Events;
using GameEntity;

namespace Game.AttackSystem.Attacks
{
    public struct AttackEvents
    {
        [Serializable]
        public class BasePlayerAttack
        {
            public UnityEvent<Player> basePlayerAttackEvent;
        }
    }
}
