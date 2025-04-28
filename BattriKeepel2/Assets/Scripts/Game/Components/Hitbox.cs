using UnityEngine;
using UnityEngine.Events;

namespace Components {
    [System.Serializable]
    public class Hitbox
    {
        private UnityEvent<Hit> m_onCollision = new UnityEvent<Hit>();
        private Transform m_transform;
        public HitboxType m_type;

        [SerializeField] private float m_size;
        [SerializeField] private Vector2 m_dimensions;
        [SerializeField] public bool isWall;

        [SerializeField] private Vector2 m_offSet = new Vector2();
        private Vector2 m_position;

        [HideInInspector] public Hit lastHitObject;

        public void Init(Transform transformHitbox) {
            CollisionManager.GetInstance().AddElement(this);
            m_transform = transformHitbox;
        }

        public Vector2 GetPosition() {
            return new Vector2(m_transform.position.x, m_transform.position.y) + m_offSet;
        }

        public float GetSize() {
            return m_size * m_transform.localScale.x;
        }

        public HitboxType GetHitboxType() {
            return m_type;
        }

        public Vector2 GetDimensions() {
            return m_dimensions * m_transform.localScale;
        }

        public void OnCollisionBehavior(Hit hit) {
            m_onCollision.Invoke(hit);
            lastHitObject = hit;
        }

        public void BindOnCollision(UnityAction<Hit> action) {
            m_onCollision.AddListener(action);
        }

        public Transform GetTransform() {
            return m_transform;
        }
    }

    public enum HitboxType {
        Circle,
        RectangularParallelepiped
    }
}
