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
        
        [Header("Stats")]
        public int BaseDamage;
        public int BaseCooldown;
        public int BaseSpeed;

        [Header("Effects")] 
        public AttackEvents.BaseAttack BaseAttack;

        public void RaiseAttack(Collider target) //Replace Collider with Entity
        {
            BaseAttack.baseAttackEvent?.Invoke(target);
        }
    }
}
