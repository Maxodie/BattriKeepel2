using System;
using UnityEngine.Events;

namespace Game.AttackSystem.Bullet
{
    public struct BulletEvents
    {
        [Serializable]
        public class BaseBullet
        {
            public UnityEvent<Bullet> baseBulletEvent;
        }
    }
}
