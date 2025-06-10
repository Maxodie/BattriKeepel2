using UnityEngine;
using Game.Entities;

namespace Game.AttackSystem.Definitions
{
    public class AttackDefinitions : ScriptableObject
    {
        // player attack

        public void PlayerMeleeAttack(Entity player)
        {
            //TODO : Fill method
        }

        public void PlayerMeleeAbility(Entity player)
        {
            //TODO : Fill method
        }

        public void PlayerMeleeUltimate(Entity player)
        {
            //TODO : Fill method
        }

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

        public void PlayerSpecialAttack(Entity player)
        {
            //TODO : Fill method
        }

        public void PlayerSpecialAbility(Entity player)
        {
            //TODO : Fill method
        }

        public void PlayerSpecialUltimate(Entity player)
        {
            //TODO : Fill method
        }
    }
}
