using UnityEngine;
using Components;
using Inputs;

public class Player : MonoBehaviour {
    [SerializeField] private InputManager m_inputManager;
    [SerializeField] private Movement m_movement;

    private void Start() {
        //m_inputManager.BindPosition(m_movement.OnPosition);
        //m_inputManager.BindPress(m_movement.OnPress);
    }

    private void FixedUpdate() {
        //m_movement.FixedUpdate();
    }

    public bool IsMoving() {
        return true;
    }
}
