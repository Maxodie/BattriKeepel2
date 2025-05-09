using GameEntity;
using UnityEngine;

namespace Game.AttackSystem.Definitions
{
    [CreateAssetMenu(fileName = "PlayerAttacksDefinition", menuName = "AttackSystem/Definitions/PlayerAttacksDefinition")]
    public class PlayerAttacksDefinition : AttackDefinitions
    {
        public void PlayerMeleeAttack(Player player)
        {
            //TODO : Fill method
        }

        public void PlayerMeleeAbility(Player player)
        {
            //TODO : Fill method
        }

        public void PlayerMeleeUltimate(Player player)
        {
            //TODO : Fill method
        }

        public void PlayerDistanceAttack(Player player)
        {
            Debug.Log("distance attack");
            player.CreateBullet();
        }

        public void PlayerDistanceAbility(Player player)
        {
            Debug.Log("distance ability");
        }

        public void PlayerDistanceUltimate(Player player)
        {
            Debug.Log("distance ultimate");
        }

        public void PlayerSpecialAttack(Player player)
        {
            //TODO : Fill method
        }

        public void PlayerSpecialAbility(Player player)
        {
            //TODO : Fill method
        }

        public void PlayerSpecialUltimate(Player player)
        {
            //TODO : Fill method
        }
    }
}
