using UnityEngine;

namespace Game.AttackSystem.Definitions
{
    [CreateAssetMenu(fileName = "PlayerAttacksDefinition", menuName = "AttackSystem/Definitions/PlayerAttacksDefinition")]
    public class PlayerAttacksDefinition : AttackDefinitions
    {
        public void PlayerMeleeAttack()
        {
            //TODO : Fill method
        }

        public void PlayerMeleeAbility()
        {
            //TODO : Fill method
        }

        public void PlayerMeleeUltimate()
        {
            //TODO : Fill method
        }

        public void PlayerDistanceAttack()
        {
            Debug.Log("distance attack");
        }

        public void PlayerDistanceAbility()
        {
            Debug.Log("distance ability");
        }

        public void PlayerDistanceUltimate()
        {
            Debug.Log("distance ultimate");
        }

        public void PlayerSpecialAttack()
        {
            //TODO : Fill method
        }

        public void PlayerSpecialAbility()
        {
            //TODO : Fill method
        }

        public void PlayerSpecialUltimate()
        {
            //TODO : Fill method
        }
    }
}
