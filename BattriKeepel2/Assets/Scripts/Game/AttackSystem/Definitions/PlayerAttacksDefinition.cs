using GameEntity;
using UnityEngine;

namespace Game.AttackSystem.Definitions
{
    [CreateAssetMenu(fileName = "PlayerAttacksDefinition", menuName = "AttackSystem/Definitions/PlayerAttacksDefinition")]
    public class PlayerAttacksDefinition : AttackDefinitions
    {
        public void PlayerDistanceAttack(Player player)
        {
            Debug.Log("distance attack");
            player.CreateBullet();
        }

        public void PlayerDistanceAbility(Player player)
        {
            Debug.Log("distance ability");
            player.LaunchAbility();
        }

        public void PlayerDistanceUltimate(Player player)
        {
            Debug.Log("distance ultimate");
        }
    }
}
