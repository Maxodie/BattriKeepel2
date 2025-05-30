using System.Threading.Tasks;
using System.Threading;
using Components;
using Game.AttackSystem.Bullet;
using Game.Entities;
using Inputs;
using UnityEngine;
using UnityEngine.Events;

namespace GameEntity
{
    public class Player : Entity {
        private InputManager m_inputManager;
        private PlayerMovement m_movement;
        private Transform transform;
        private PlayerGraphics m_playerGraphics;
        private Rigidbody2D m_rb;

        private SO_PlayerData playerData;

        private UnityEvent<Player> m_singleTapEvent = new();
        private UnityEvent<Player> m_doubleTapEvent = new();
        private UnityEvent<Player> m_shakeEvent = new();

        public Player(SO_PlayerData data, Transform spawnPoint)
        {
            entityType = EntityType.Player;
            playerData = data;

            Init(spawnPoint);
            BindActions();
        }

        private void Init(Transform spawnPoint) {
            m_movement = new PlayerMovement();
            m_playerGraphics = GraphicsManager.Get().GenerateVisualInfos<PlayerGraphics>(playerData.playerGraphics, spawnPoint, this);
            m_rb = m_playerGraphics.rb;
            m_inputManager = m_playerGraphics.inputManager;
            transform = m_playerGraphics.transform;
            m_movement.rb = m_rb;

            attacks = playerData.attackSet;
            base.Init(attacks);
        }

        private void BindActions() {
            m_inputManager.BindPosition(m_movement.OnPosition);
            m_inputManager.BindPress(m_movement.OnPress);
            m_inputManager.BindTap(TapReceived);
            BindDoubleTap(attacks.AbilityAttack.RaiseAttack);
            BindShake(attacks.UltimateAttack.RaiseAttack);
        }

        private void BindSingleTap(UnityAction<Player> action) {
            m_singleTapEvent.AddListener(action);
        }

        private void BindDoubleTap(UnityAction<Player> action) {
            m_doubleTapEvent.AddListener(action);
        }

        private void BindShake(UnityAction<Player> action) {
            m_shakeEvent.AddListener(action);
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
            m_doubleTapEvent?.Invoke(this);
            cts.Cancel();
            tapState = false;
        }

        private async Task TapTimer(CancellationToken token)
        {
            await Task.Delay(500, token);
            Log.Success("single tap");
            if (!token.IsCancellationRequested)
            {
                tapState = false;
                m_singleTapEvent?.Invoke(this);
            }
        }

        public void Update() {
            m_movement.HandleMovement();
        }

        public bool IsScreenPressed() {
            return m_movement.IsScreenPressed();
        }

        public override void CreateBullet()
        {
            BulletData bulletData = new BulletData();

            bulletData.Owner = this;
            bulletData.BulletBehaviour = playerData.bulletBehaviour;
            bulletData.Speed = playerData.attackSet.BasicAttack.BaseSpeed;
            bulletData.Damage = playerData.attackSet.BasicAttack.BaseDamage;

            Bullet newBullet = new Bullet(bulletData, transform);
        }

        public override void TakeDamage(Bullet bullet) {

        }

        public override void Die() {

        }
    }
}
