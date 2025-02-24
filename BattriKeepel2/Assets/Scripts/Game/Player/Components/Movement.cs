using UnityEngine;

namespace Components {
    [System.Serializable]
    public class Movement {
        [Header("data")] // replace with scriptable object
        [SerializeField] float m_movementMultiplier = 1;

        [SerializeField] public Transform m_transform;

        Vector2 m_movementDirection;

        public void OnDelta(Vector2 delta) {
            m_movementDirection = delta.normalized;
        }

        private void HandleMovement() {
            Vector3 newPosition = new Vector3(m_movementDirection.x * m_movementMultiplier, m_movementDirection.y * m_movementMultiplier, 0);
            m_transform.position += newPosition;
        }

        public void Update() {
            HandleMovement();
        }
    }
}
