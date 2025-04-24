using System;
using Components;
using Game.AttackSystem.Bullet;
using Game.Entities;
using Inputs;
using UnityEngine;

namespace Game.Player
{
    [Serializable]
    public class Player : Entity
    {
        public Player(Transform spawnTransform) {
            BindActions();
        }

        [SerializeField] private InputManager m_inputManager;
        [SerializeField] private PlayerMovement m_movement;
        [SerializeField] private Hitbox m_hitBox;

        protected override void Init() {
            base.Init();
            m_hitBox.Init(GetEntityGraphics().transform);
            m_movement.m_transform = GetEntityGraphics().transform;
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
    
        public override void TakeDamage(Bullet bullet)
        {
        
        }
    }
}
