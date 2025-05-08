using UnityEngine;

namespace Components {
    public class PlayerMovement : Movement {
        public Transform m_transform;
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

        public override Vector2 WishMovement() {
            if (m_isPressed == UnityEngine.InputSystem.TouchPhase.Began
                    || m_isPressed == UnityEngine.InputSystem.TouchPhase.Ended
                    || m_isPressed == UnityEngine.InputSystem.TouchPhase.None) {
                return Vector2.zero;
            }

            vel = newPos - dirtyPos;
            dirtyPos = newPos;

            return new Vector3(vel.x, vel.y, m_transform.position.z);
        }

        public override void ApplyMovement(Vector2 velocity) {
            m_transform.position += new Vector3(velocity.x, velocity.y, m_transform.position.z);
        }

        public bool IsScreenPressed() {
            if (m_isPressed != UnityEngine.InputSystem.TouchPhase.None && m_isPressed != UnityEngine.InputSystem.TouchPhase.Ended) {
                return true;
            }
            return false;
        }
    }
}

