using UnityEngine;

namespace Components {
    public class PlayerMovement {
        public Rigidbody2D rb;
        private Vector2 newPos = new Vector2();
        private Vector2 dirtyPos = Vector2.zero;
        public Vector2 vel;
        private UnityEngine.InputSystem.TouchPhase m_isPressed;

        public void OnPosition(Vector2 position) {
            newPos = Camera.main.ScreenToWorldPoint(position);

            if (m_isPressed == UnityEngine.InputSystem.TouchPhase.Began) {
                dirtyPos = newPos;
                vel = Vector2.zero;
            }
        }

        public void OnPress(UnityEngine.InputSystem.TouchPhase state) {
            m_isPressed = state;
        }

        public void HandleMovement() {
            if (m_isPressed == UnityEngine.InputSystem.TouchPhase.Began
                    || m_isPressed == UnityEngine.InputSystem.TouchPhase.Ended
                    || m_isPressed == UnityEngine.InputSystem.TouchPhase.None) {
                rb.linearVelocity = Vector2.zero;
            }

            vel = (newPos - dirtyPos) / Time.deltaTime;
            dirtyPos = newPos;

            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, vel, .7f);
        }

        public bool IsScreenPressed() {
            if (m_isPressed != UnityEngine.InputSystem.TouchPhase.None && m_isPressed != UnityEngine.InputSystem.TouchPhase.Ended) {
                return true;
            }
            return false;
        }
    }
}

