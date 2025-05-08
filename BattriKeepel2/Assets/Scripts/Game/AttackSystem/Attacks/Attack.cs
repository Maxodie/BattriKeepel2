using Game.Entities;
using UnityEngine;

namespace Game.AttackSystem.Attacks
{
    [CreateAssetMenu(fileName = "Attack", menuName = "AttackSystem/Attack")]
    public class Attack : ScriptableObject
    {
        [Header("Generic")]
        public string DisplayName;
        public string Slug;
        [TextArea] public string Description;
        public bool needTarget;

        [Header("Stats")]
        public int BaseDamage;
        public int BaseCooldown;
        public int BaseSpeed;

        [Header("Effects")]
        public AttackEvents.BaseAttack BaseAttack;

        public void RaiseAttack()
        {
            BaseAttack.baseAttackEvent?.Invoke();
        }
    }
}
