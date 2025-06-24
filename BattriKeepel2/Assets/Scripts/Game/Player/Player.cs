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
        bool m_isActive = true;
        bool m_isDestroyed = false;

        private bool isUltimateReady = true;
        private bool isCapacityCurrent;
        private bool isInvincible;

        AttackGraphicsPool m_bulletPool;
        public Vector3 position;

        public Player(AttackGraphicsPool bulletPool, SO_PlayerData data, Transform spawnPoint)
        {
            MaxHealth = data.maxHealth;
            Health = MaxHealth;
            entityType = EntityType.Player;
            playerData = data;
            m_isActive = true;
            m_bulletPool = bulletPool;

            Init(spawnPoint);
            BindActions();
        }

        private void Init(Transform spawnPoint) {
            m_movement = new PlayerMovement();
            m_playerGraphics = GraphicsManager.Get().GenerateVisualInfos<PlayerGraphics>(playerData.playerGraphics, spawnPoint, this);
            m_playerGraphics.transform.position = GraphicsManager.Get().GetCameraLocation((int)SpawnDir.South) + new Vector2(0, 2);
            m_rb = m_playerGraphics.rb;
            m_inputManager = m_playerGraphics.inputManager;
            transform = m_playerGraphics.transform;
            m_movement.rb = m_rb;

            m_playerGraphics.SetPlayer(this);

            base.Init(playerData.attackSet, m_playerGraphics);

            soundInstance = AudioManager.CreateSoundInstance(false, false);
        }

        private void BindActions() {
            m_inputManager.BindPosition(m_movement.OnPosition);
            m_inputManager.BindPress(m_movement.OnPress);
            m_inputManager.BindTap(TapReceived);
            m_inputManager.BindShake(OnShakeReceived);
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

        bool shook = false;

        private void OnShakeReceived(float shake)
        {
            if (shook) {
                return;
            }

            m_shakeEvent?.Invoke(this);
            shook = true;
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
            if(!m_isActive)
            {
                return;
            }

            position = m_movement.rb.position;
            m_movement.HandleMovement();
            for (int i = 0; i < m_bullets.Count; i++) {
                if (m_bullets[i].IsDead()) {
                    m_bullets.RemoveAt(i);
                    i--;
                    continue;
                }

                m_bullets[i].Update();
            }
        }

        public bool IsScreenPressed() {
            return m_movement.IsScreenPressed();
        }

        public override void CreateBullet()
        {
            if(m_isActive)
            {
                if (isCapacityCurrent)
                {
                    Debug.Log(playerData.ultimateBulletData.damage);
                }
                m_bullets.Add(new Bullet(m_bulletPool, isCapacityCurrent ? playerData.ultimateBulletData : playerData.attackBulletData, transform.position + Vector3.up * .2f, m_playerGraphics.transform, false, Vector3.up, typeof(BossEntity)));
            }
        }

        public async Awaitable LaunchAbility()
        {
            if (!isAbilityReady || isCapacityCurrent) return;

            isCapacityCurrent = true; //IsCapacityCurrent permet de voir si l'ability ou l'ultimate est en cours pour empecher de pouvoir lancer les 2 en meme temps
            SetInvincibility(true);
            m_playerGraphics.StartCoroutine(ReloadAbility());

            attackManager.CancelAttack();

            await Awaitable.WaitForSecondsAsync(playerData.attackSet.AbilityAttack.BaseCooldown);

            m_bullets.Add(new Bullet(m_bulletPool, playerData.abilityBulletData, transform.position + Vector3.up * .2f, m_playerGraphics.transform, false, Vector3.up, typeof(BossEntity)));

            isCapacityCurrent = false;
            SetInvincibility(false);
            attackManager.StartAttacking();
        }

        public async Awaitable LaunchUltimate()
        {
            if (!isUltimateReady || isCapacityCurrent) return;

            isCapacityCurrent = true; //IsCapacityCurrent permet de voir si l'ability ou l'ultimate est en cours pour empecher de pouvoir lancer les 2 en meme temps
            isUltimateReady = false;
            m_playerGraphics.SetUltimateFill(0.0f);

            attackManager.CancelAttack();
            attackManager.StartUltimate();

            await Awaitable.WaitForSecondsAsync(playerData.attackSet.UltimateAttack.BaseDuration);

            isCapacityCurrent = false;
            attackManager.CancelAttack();

            attackManager.StartAttacking();
        }

        private IEnumerator ReloadAbility()
        {
            isAbilityReady = false;
            float timer = playerData.attackSet.AbilityAttack.BaseReloadTime;
            while(true)
            {
                m_playerGraphics.SetAbilityFill(timer / playerData.attackSet.AbilityAttack.BaseReloadTime);
                yield return null;
                timer += Time.deltaTime;
                if(timer <= 0)
                {
                    break;
                }
            }
            isAbilityReady = true;
        }

        private void SetInvincibility(bool invincibilty)
        {
            isInvincible = invincibilty;
            m_playerGraphics.SetTransparency(invincibilty);
        }

        public bool GetInvincibility()
        {
            return isInvincible;
        }

        public override void TakeDamage(float amount) {
            MobileEffect.VibrationEffect(MobileEffectVibration.SMALL);

            Health -= amount;
            if(Health <= 0)
            {
                m_isActive = false;
                Die();
            }
            else if(Health > MaxHealth)
            {
                Health = MaxHealth;
            }
        }

        public bool IsDead()
        {
            return Health <= 0;
        }

        public override void Die()
        {
            MobileEffect.VibrationEffect(MobileEffectVibration.BIG);
            MobileEffect.SetOnFlashlight(true, 1.5f);

            Destroy();
        }

        public void SetActive(bool state)
        {
            if(m_isDestroyed)
            {
                m_isActive = false;
                return;
            }

            m_isActive = state;
        }

        public void Destroy()
        {
            if(!m_isDestroyed)
            {
                Object.Destroy(m_playerGraphics.gameObject);
                AudioManager.DestroySoundInstance(soundInstance);
                m_isDestroyed = true;
                m_isActive = false;
                ClearBullets();
            }
        }

        public void ClearBullets()
        {
            for(int i = 0; i < m_bullets.Count; i++)
            {
                m_bullets[i].Kill();
            }

            m_bullets.Clear();
        }
    }
}
