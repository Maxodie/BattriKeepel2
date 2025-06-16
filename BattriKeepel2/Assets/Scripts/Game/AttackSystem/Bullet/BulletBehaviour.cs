using UnityEngine;

    [CreateAssetMenu(fileName = "BulletBehaviour", menuName = "AttackSystem/Bullet/BulletBehaviour")]
    public class BulletBehaviour : ScriptableObject
    {
        [SerializeField] private bool needConstantUpdate;
        public bool NeedConstantUpdate => needConstantUpdate;

        public BulletEvents.BaseBullet BaseBullet;

        public void RaiseBullet(Bullet bullet)
        {
            BaseBullet.baseBulletEvent?.Invoke(bullet);
        }
    }
