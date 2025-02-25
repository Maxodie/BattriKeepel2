using UnityEngine;
using UnityEngine.Events;

namespace Components {
    [System.Serializable]
    public class Hitbox
    {
        private UnityEvent<Transform> m_onCollision = new UnityEvent<Transform>();
        [HideInInspector] public Transform m_transform;

        [SerializeField] private float m_size;
        [SerializeField] private Vector2 m_offSet = new Vector2();
        private Vector2 m_position;

        public Transform lastHitObject;

        public void Start() {
            // add instance to collision manager
        }

        public Vector2 GetPosition() {
            return new Vector2(m_transform.position.x, m_transform.position.y) + m_offSet;
        }

        public float GetSize() {
            return m_size;
        }

        public void OnCollisionBehavior(Transform hit) {
            m_onCollision.Invoke(hit);
            lastHitObject = hit;
        }

        public void BindOnCollision(UnityAction<Transform> action) {
            m_onCollision.AddListener(action);
        }
    }
}
