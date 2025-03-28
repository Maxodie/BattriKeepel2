using System;
using UnityEngine;
using UnityEngine.Events;

namespace Game.AttackSystem.Attacks
{
    public struct AttackEvents
    {
        [Serializable]
        public class BaseAttack
        {
            public UnityEvent<Collider> baseAttackEvent;
        }
    }
}
