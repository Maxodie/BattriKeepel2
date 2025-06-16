using System;
using UnityEngine.Events;

    public struct BulletEvents
    {
        [Serializable]
        public class BaseBullet
        {
            public UnityEvent<Bullet> baseBulletEvent;
        }
    }
