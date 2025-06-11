using UnityEngine;
using Game.Entities;

namespace Game.AttackSystem.Definitions
{
    public class AttackDefinitions : ScriptableObject
    {
        // player attack

        public void PlayerDistanceAttack(Entity player)
        {
            Debug.Log("distance attack");
        }

        public void PlayerDistanceAbility(Entity player)
        {
            Debug.Log("distance ability");
        }

        public void PlayerDistanceUltimate(Entity player)
        {
            Debug.Log("distance ultimate");
        }
    }
}
