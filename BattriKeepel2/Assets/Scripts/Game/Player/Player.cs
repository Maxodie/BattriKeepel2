using System.Threading.Tasks;
using System.Threading;
using Components;
using Game.AttackSystem.Bullet;
using Game.Entities;
using Inputs;
using Unity.VisualScripting;
using UnityEngine;

namespace GameEntity
{
    public class Player : Entity {
        private InputManager m_inputManager;
        private PlayerMovement m_movement;
        private CameraEffect m_cameraEffect;
        public Hitbox m_hitBox;
        private Transform transform;
        private PlayerGraphics m_playerGraphics;
        private Transform m_cameraTransform;

        private Vector2 m_currentVel = new Vector2();

        public Player(SO_PlayerData data, Transform spawnPoint, Transform cameraTransform)
        {
            entityType = EntityType.Player;
            
            Init(data, spawnPoint, cameraTransform);
            BindActions();
        }

        public override void TakeDamage(Bullet bullet) {

        }

        public override void Die() {

        }

        private void Init(SO_PlayerData data, Transform spawnPoint, Transform cameraTransform) {
            m_playerGraphics = GraphicsManager.Get().GenerateVisualInfos<PlayerGraphics>(data.playerGraphics, spawnPoint, this);
            m_inputManager = m_playerGraphics.inputManager;
            transform = m_playerGraphics.m_playerTransform;

            attacks = data.attackSet;
            bulletData = data.bulletData;
            base.Init(attacks);
            
            m_cameraTransform = cameraTransform;
            m_cameraEffect = new CameraEffect(m_cameraTransform, data.shakeAmount, data.shakeSpeed);

            m_hitBox = data.hitBox;
            m_hitBox.Init(transform);

            m_movement = new PlayerMovement();
            m_movement.m_transform = transform;
        }

        private void BindActions() {
            m_inputManager.BindPosition(m_movement.OnPosition);
            m_inputManager.BindPress(m_movement.OnPress);
            m_inputManager.BindTap(TapReceived);
            m_hitBox.BindOnCollision(HandleCollisions);
        }

        bool tapState = false;
        CancellationTokenSource cts = new CancellationTokenSource();

        private void TapReceived()
        {
            if (!tapState)
            {
                cts = new CancellationTokenSource();
                Task.Run(() => TapTimer(cts.Token), cts.Token);
                tapState = true;
                return;
            }
            Log.Success("double tap");
            attacks.UltimateAttack.RaiseAttack();
            cts.Cancel();
            tapState = false;
        }

        private async Task TapTimer(CancellationToken token)
        {
            await Task.Delay(500, token);
            Log.Success("single tap");
            attacks.AbilityAttack.RaiseAttack();
            if (!token.IsCancellationRequested)
            {
                tapState = false;
                // call event (for the parry)
            }
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
