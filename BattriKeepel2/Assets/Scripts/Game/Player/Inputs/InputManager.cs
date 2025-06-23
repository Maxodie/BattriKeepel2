using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Inputs {
    public class InputManager : MonoBehaviour
    {
        PlayerInput m_playerInputs;

        UnityEvent<Vector2> m_onPosition = new UnityEvent<Vector2>();
        UnityEvent<UnityEngine.InputSystem.TouchPhase> m_onPress = new UnityEvent<UnityEngine.InputSystem.TouchPhase>();
        UnityEvent<Vector3> m_onShake = new UnityEvent<Vector3>();
        UnityEvent m_onTap = new UnityEvent();

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
                                .Player             //LMAOOOOOOO
                                .Position           //GET REKT NERDS
                                .performed          //TRY READING THE CODE NOW
                                += OnPosition;

            m_playerInputs
                .Player
                .Shake
                .performed
                += OnShake;

                            m_playerInputs
                                .Player
                                .Tap
                                .performed
                                += OnTap;
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

        void OnShake(InputAction.CallbackContext context) {
            Vector3 shake = context.ReadValue<Vector3>();
            Log.Info(shake.magnitude);
            if(shake.magnitude >= 5)
            {
                m_onShake.Invoke(shake);
            }
        }

        void OnTap(InputAction.CallbackContext context) {
            m_onTap.Invoke();
        }

        public void BindPress(UnityAction<UnityEngine.InputSystem.TouchPhase> action) {
            m_onPress.AddListener(action);
        }

        public void BindPosition(UnityAction<Vector2> action) {
            m_onPosition.AddListener(action);
        }

        public void BindShake(UnityAction<Vector3> action) {
            m_onShake.AddListener(action);
        }

        public void BindTap(UnityAction action) {
            m_onTap.AddListener(action);
        }
    }
}

