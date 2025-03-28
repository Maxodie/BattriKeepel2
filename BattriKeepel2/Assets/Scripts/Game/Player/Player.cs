using UnityEngine;
using Components;
using Inputs;

public class Player : MonoBehaviour {
    [SerializeField] private InputManager m_inputManager;
    [SerializeField] private Movement m_movement;
    [SerializeField] private Hitbox m_hitBox;

    private void Start() {
        Init();
        BindActions();
    }

    private void Init() {
        m_hitBox.Start();
        m_hitBox.m_transform = this.transform;
        m_movement.m_transform = this.transform;
    }

    private void BindActions() {
        m_inputManager.BindPosition(m_movement.OnPosition);
        m_inputManager.BindPress(m_movement.OnPress);
        m_hitBox.BindOnCollision((other) => {Log.Info("caca");});
    }

    private void Update() {
        m_movement.Update();
    }

    public bool IsScreenPressed() {
        return m_movement.IsScreenPressed();
    }
}
