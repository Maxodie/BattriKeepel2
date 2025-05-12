using System.Collections.Generic;
using Game.Entities;
using UnityEngine;

namespace Game.Managers
{
    public class EnemyManager : GameEntityMonoBehaviour
    {
        public static EnemyManager Instance;
        
        [HideInInspector] public List<Entity> currentEnemies;

        public void AddEnemy(Entity enemy) //Replace Collider with Enemy
        {
            currentEnemies.Add(enemy);
        }

        public void RemoveEnemy(Entity enemy) //Replace Collider with Enemy
        {
            currentEnemies.Remove(enemy);
        }
    }
}
