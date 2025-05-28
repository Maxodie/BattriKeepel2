using System;
using UnityEngine.Events;
using Game.Entities;

namespace Game.AttackSystem.Attacks
{
    public struct AttackEvents
    {
        [Serializable]
        public class BasePlayerAttack
        {
            public UnityEvent<Entity> basePlayerAttackEvent;
        }
    }
}
