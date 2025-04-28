using Components;
using Game.Entities;
using Inputs;
using UnityEngine;

namespace GameEntity
{
    public class Player : Entity {
        private InputManager m_inputManager;
        private PlayerMovement m_movement;
        private Hitbox m_hitBox;
        private Transform transform;
        private PlayerGraphics m_playerGraphics;

        private Vector2 m_currentTarget = new Vector2();

        public Player(SO_PlayerData data, Transform spawnPoint) {
            Init(data, spawnPoint);
            BindActions();
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
            m_movement.HandleMovement(m_currentTarget);
            m_currentTarget = m_movement.targetPosition;
        }

        public bool IsScreenPressed() {
            return m_movement.IsScreenPressed();
        }

        private void HandleCollisions(Hit other) {
            Wall wall = other.hitObject.gameObject.GetComponent<Wall>();
            if (wall != null) {
                Hitbox hit = wall.m_hitBox;
                Vector2 hitPosition = hit.GetPosition();
                Vector2 playerPosition = this.transform.position;

                if (hit.GetHitboxType() == HitboxType.Circle) {
                    float radius = hit.GetSize();
                    float distance = Vector2.Distance(hitPosition, playerPosition);

                    Vector2 direction = (playerPosition - hitPosition).normalized;
                    m_currentTarget = playerPosition + direction * (radius - distance);
                } else {
                    if (Mathf.Abs(hitPosition.x - playerPosition.x) < Mathf.Abs(hitPosition.y - playerPosition.y)) {
                        m_currentTarget.y = playerPosition.y;
                    } else {
                        m_currentTarget.x = playerPosition.x;
                    }
                }
            }
        }
    }
}
