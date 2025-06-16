using UnityEngine;
using UnityEngine.UI;

namespace Game.AttackSystem.Bullet
{
    public class BulletGraphics : GameEntityGraphics
    {
        [SerializeField] public Image sprite;
        [SerializeField] public SO_BulletData data;
    }
}
