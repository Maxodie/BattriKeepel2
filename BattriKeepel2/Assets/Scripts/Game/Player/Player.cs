using UnityEngine;
using Components;
using Inputs;

public class Player : MonoBehaviour {
    [SerializeField] private InputManager m_inputManager;
    [SerializeField] private Movement m_movement;

    private void Start() {
        m_inputManager.BindDelta(m_movement.OnDelta);
    }

    private void Update() {
        m_movement.Update();
    }
}
