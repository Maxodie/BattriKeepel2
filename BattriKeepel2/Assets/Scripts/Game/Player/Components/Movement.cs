using UnityEngine;

namespace Components {
    [System.Serializable]
    public class Movement {
        [Header("data")] // replace with scriptable object
        [SerializeField] float m_movementMultiplier = 1;

        [SerializeField] public Transform m_transform;

        Vector2 m_newPos = new Vector2();
        Vector2 m_offSet = new Vector2();
        UnityEngine.InputSystem.TouchPhase m_isPressed;

        public void OnPosition(Vector2 position) {
            m_newPos = Camera.main.ScreenToWorldPoint(position);

            Debug.Log(m_newPos);
            if (m_isPressed != UnityEngine.InputSystem.TouchPhase.Moved) {
                m_offSet = m_newPos - new Vector2(m_transform.position.x, m_transform.position.y);
            }
        }

        public void OnPress(UnityEngine.InputSystem.TouchPhase state) {
            m_isPressed = state;
        }

        private void HandleMovement() {
            if (m_isPressed == UnityEngine.InputSystem.TouchPhase.Began
                    || m_isPressed == UnityEngine.InputSystem.TouchPhase.Ended
                    || m_isPressed == UnityEngine.InputSystem.TouchPhase.None) {
                return;
            }
            m_transform.position = new Vector3(m_newPos.x - m_offSet.x, m_newPos.y - m_offSet.y, 0);
        }

        public void FixedUpdate() {
            HandleMovement();
        }
    }
}
