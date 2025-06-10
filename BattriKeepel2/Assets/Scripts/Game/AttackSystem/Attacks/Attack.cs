using UnityEngine;
using Game.Entities;

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
        public AudioClip attackSound;

        [Header("Stats")]
        public int BaseDamage;
        public int BaseCooldown;
        public int BaseSpeed;

        [Header("Effects")]
        public AttackEvents.BasePlayerAttack BasePlayerAttack;

        public void RaiseAttack(Entity entity)
        {
            BasePlayerAttack.basePlayerAttackEvent?.Invoke(entity);
        }
    }
}
