using System.Collections;
using System.Threading.Tasks;
using System.Threading;
using Components;
using Game.Entities;
using Inputs;
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

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

        private SoundInstance soundInstance;
        private List<Bullet> m_bullets = new();

        private bool isAbilityReady = true;

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

            base.Init(playerData.attackSet, m_playerGraphics);

            soundInstance = AudioManager.CreateSoundInstance(false, false);
        }

        private void BindActions() {
            m_inputManager.BindPosition(m_movement.OnPosition);
            m_inputManager.BindPress(m_movement.OnPress);
            m_inputManager.BindTap(TapReceived);
            BindDoubleTap(attackManager.attacks.AbilityAttack.RaiseAttack);
            BindShake(attackManager.attacks.UltimateAttack.RaiseAttack);
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
            soundInstance.PlaySound(playerData.doubleTapAttackSound);
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
                soundInstance.PlaySound(playerData.singleTapAttackSound);
            }
        }

        public void Update() {
            m_movement.HandleMovement();
            for (int i = 0; i < m_bullets.Count; i++) {
                if (m_bullets[i].IsDead()) {
                    m_bullets.RemoveAt(i);
                    i--;
                }
            }

            foreach(Bullet bullet in m_bullets)
            {
                bullet.Update();
            }
        }

        public bool IsScreenPressed() {
            return m_movement.IsScreenPressed();
        }

        public override void CreateBullet()
        {
            m_bullets.Add(new Bullet(playerData.bulletGraphics.data, transform.position + Vector3.up * .2f, m_playerGraphics.transform, false, Vector3.up, typeof(BossEntity)));
        }

        public async Awaitable LaunchAbility()
        {
            if (!isAbilityReady) return;

            m_playerGraphics.StartCoroutine(ReloadAbility());

            attackManager.CancelAttack();

            await Awaitable.WaitForSecondsAsync(playerData.attackSet.AbilityAttack.BaseCooldown);

            m_bullets.Add(new Bullet(playerData.bulletGraphics.data, transform.position + Vector3.up * .2f, m_playerGraphics.transform, false, Vector3.up, typeof(BossEntity)));
            attackManager.StartAttacking();
        }

        private IEnumerator ReloadAbility()
        {
            isAbilityReady = false;
            yield return new WaitForSeconds(playerData.attackSet.AbilityAttack.BaseReloadTime);
            isAbilityReady = true;
        }

        public override void TakeDamage(float bullet) {
            MobileEffect.VibrationEffect(MobileEffectVibration.SMALL);
        }

        public override void Die()
        {
            MobileEffect.VibrationEffect(MobileEffectVibration.BIG);
            MobileEffect.SetOnFlashlight(true, 0.5f);

            AudioManager.DestroySoundInstance(soundInstance);
        }
    }
}
