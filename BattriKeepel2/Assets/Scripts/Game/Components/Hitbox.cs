using UnityEngine;
using UnityEngine.Events;

namespace Components {
    [System.Serializable]
    public class Hitbox
    {
        private UnityEvent<Transform> m_onCollision = new UnityEvent<Transform>();
        private Transform m_transform;
        public HitboxType m_type;

        [SerializeField] private float m_size;
        [SerializeField] private Vector2 m_dimensions;

        [SerializeField] private Vector2 m_offSet = new Vector2();
        private Vector2 m_position;

        [HideInInspector] public Transform lastHitObject;
        
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

        public Vector2 GetDimensions() {
            return m_dimensions * m_transform.localScale;
        }

        public void OnCollisionBehavior(Transform hit) {
            m_onCollision.Invoke(hit);
            lastHitObject = hit;
        }

        public void BindOnCollision(UnityAction<Transform> action) {
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
