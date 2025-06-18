using GameEntity;
using UnityEngine;

namespace Game.AttackSystem.Definitions
{
    [CreateAssetMenu(fileName = "PlayerAttacksDefinition", menuName = "AttackSystem/Definitions/PlayerAttacksDefinition")]
    public class PlayerAttacksDefinition : AttackDefinitions
    {
        public void PlayerDistanceAttack(Player player)
        {
            player.CreateBullet();
        }

        public void PlayerDistanceAbility(Player player)
        {
            player.LaunchAbility();
        }

        public void PlayerDistanceUltimate(Player player)
        {
            Debug.Log("distance ultimate");
            player.LaunchUltimate();
        }
    }
}
