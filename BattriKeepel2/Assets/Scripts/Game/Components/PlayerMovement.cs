using UnityEngine;

namespace Components {
    [System.Serializable]
    public class PlayerMovement : Movement {

        [HideInInspector] public Transform m_transform;
        private Vector2 newPos = new Vector2();
        private Vector2 offset = Vector2.zero;
        private UnityEngine.InputSystem.TouchPhase m_isPressed;

        public void OnPosition(Vector2 position) {
            newPos = Camera.main.ScreenToWorldPoint(position);

            if (m_isPressed == UnityEngine.InputSystem.TouchPhase.Began) {
                offset = newPos - new Vector2(m_transform.position.x, m_transform.position.y);
            }
        }

        public void OnPress(UnityEngine.InputSystem.TouchPhase state) {
            m_isPressed = state;
        }

        public Vector2 targetPosition = new Vector2();

        public override void HandleMovement(Vector2 target) {
            if (m_isPressed == UnityEngine.InputSystem.TouchPhase.Began
                    || m_isPressed == UnityEngine.InputSystem.TouchPhase.Ended
                    || m_isPressed == UnityEngine.InputSystem.TouchPhase.None) {
                return;
            }

            targetPosition = newPos - offset;

            Debug.Log(target);

            m_transform.position = new Vector3(target.x, target.y, m_transform.position.z);
        }

        public bool IsScreenPressed() {
            if (m_isPressed != UnityEngine.InputSystem.TouchPhase.None && m_isPressed != UnityEngine.InputSystem.TouchPhase.Ended) {
                return true;
            }
            return false;
        }
    }
}

