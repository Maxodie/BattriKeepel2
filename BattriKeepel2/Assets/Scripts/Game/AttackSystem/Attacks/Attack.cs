using UnityEngine;
using Game.Entities;
using GameEntity;

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
        public float BaseDamage;
        public float BaseCooldown;
        public float BaseSpeed;

        [Header("Effects")]
        public AttackEvents.BasePlayerAttack BasePlayerAttack;

        public void RaiseAttack(Player player)
        {
            BasePlayerAttack.basePlayerAttackEvent?.Invoke(player);
        }
    }
}
