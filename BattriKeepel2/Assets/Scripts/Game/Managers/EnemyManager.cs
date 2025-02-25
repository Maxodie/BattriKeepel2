using System.Collections.Generic;
using UnityEngine;

namespace Game.Managers
{
    public class EnemyManager : MonoBehaviour
    {
        public static EnemyManager Instance;
        
        [HideInInspector] public List<Collider> currentEnemies;

        public void AddEnemy(Collider enemy) //Replace Collider with Enemy
        {
            currentEnemies.Add(enemy);
        }

        public void RemoveEnemy(Collider enemy) //Replace Collider with Enemy
        {
            currentEnemies.Remove(enemy);
        }
    }
}
