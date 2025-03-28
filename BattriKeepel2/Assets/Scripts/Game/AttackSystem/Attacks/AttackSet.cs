using UnityEngine;

namespace Game.AttackSystem.Attacks
{
    [CreateAssetMenu(fileName = "AttackSet", menuName = "AttackSystem/AttackSet")]
    public class AttackSet : ScriptableObject
    {
        public Attack BasicAttack;
        public Attack AbilityAttack;
        public Attack UltimateAttack;
    }
}
