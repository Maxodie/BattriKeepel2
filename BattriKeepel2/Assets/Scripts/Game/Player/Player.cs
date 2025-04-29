using Components;
using Game.AttackSystem.Bullet;
using Game.Entities;
using Inputs;
using UnityEngine;

namespace GameEntity
{
    public class Player : Entity {
        private InputManager m_inputManager;
        private PlayerMovement m_movement;
        public Hitbox m_hitBox;
        private Transform transform;
        private PlayerGraphics m_playerGraphics;

        private Vector2 m_currentVel = new Vector2();

        public Player(SO_PlayerData data, Transform spawnPoint) {
            Init(data, spawnPoint);
            BindActions();
        }

        public override void TakeDamage(Bullet bullet) {

        }

        public override void Die() {

        }

        private void Init(SO_PlayerData data, Transform spawnPoint) {
            m_movement = new PlayerMovement();
            m_hitBox = data.hitBox;
            m_playerGraphics = GraphicsManager.Get().GenerateVisualInfos<PlayerGraphics>(data.playerGraphics, spawnPoint, this);
            m_inputManager = m_playerGraphics.inputManager;
            transform = m_playerGraphics.transform;
            m_hitBox.Init(transform);
            m_movement.m_transform = transform;
        }

        private void BindActions() {
            m_inputManager.BindPosition(m_movement.OnPosition);
            m_inputManager.BindPress(m_movement.OnPress);
            m_hitBox.BindOnCollision(HandleCollisions);
        }

        public void Update() {
            m_movement.HandleMovement(m_currentVel);
            m_currentVel = m_movement.vel;
        }

        public bool IsScreenPressed() {
            return m_movement.IsScreenPressed();
        }

        private void HandleCollisions(Hit other) {
            Wall wall = other.hitObject.gameObject.GetComponent<Wall>();
            if (wall != null) {
                Vector2 playerPosition = transform.position;
                Vector2 collisionPoint = other.hitPosition;

                Vector2 directionToCollision = (collisionPoint - playerPosition).normalized;

                float depth = Vector2.Distance(collisionPoint, m_hitBox.GetClosestPoint(wall.m_hitBox));

                if (depth <= 0.001 && depth >= -0.001) {
                    depth = 0;
                }

                AdjustVelocityToAvoidCollision(directionToCollision, depth / 2);
            }
        }

        private void AdjustVelocityToAvoidCollision(Vector2 directionToAvoid, float depth) {
            m_currentVel -= directionToAvoid * depth;
        }
    }
}
