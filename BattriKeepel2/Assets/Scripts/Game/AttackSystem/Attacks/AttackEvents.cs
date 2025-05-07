using System;
using Game.Entities;
using UnityEngine.Events;

namespace Game.AttackSystem.Attacks
{
    public struct AttackEvents
    {
        [Serializable]
        public class BaseAttack
        {
            public UnityEvent baseAttackEvent;
        }
    }
}
