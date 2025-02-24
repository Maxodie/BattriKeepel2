using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Inputs {
    public class InputManager : MonoBehaviour
    {
        PlayerInput m_playerInputs;

        UnityEvent<Vector2> m_onPosition = new UnityEvent<Vector2>();
        UnityEvent<UnityEngine.InputSystem.TouchPhase> m_onPress = new UnityEvent<UnityEngine.InputSystem.TouchPhase>();

        void Awake() {
            m_playerInputs = new PlayerInput();
        }

        void OnEnable() {
            m_playerInputs
                .Enable();

            m_playerInputs
                .Player
                .Press
                .performed
                += OnPress;

            m_playerInputs
                .Player
                .Position
                .performed
                += OnPosition;
        }

        void OnDisable() {
            m_playerInputs.Disable();
        }

        void OnPress(InputAction.CallbackContext context) {
            m_onPress.Invoke(context.ReadValue<UnityEngine.InputSystem.TouchPhase>());
        }

        void OnPosition(InputAction.CallbackContext context) {
            Vector2 position = context.ReadValue<Vector2>();
            m_onPosition.Invoke(position);
        }

        public void BindPress(UnityAction<UnityEngine.InputSystem.TouchPhase> action) {
            m_onPress.AddListener(action);
        }

        public void BindPosition(UnityAction<Vector2> action) {
            m_onPosition.AddListener(action);
        }
    }
}

