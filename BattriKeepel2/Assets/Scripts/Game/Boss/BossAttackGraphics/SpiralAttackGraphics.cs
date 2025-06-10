using System.Collections.Generic;
using Game.AttackSystem.Bullet;
using UnityEngine;

public class SpiralAttackGraphics : BossAttackGraphics {
    [SerializeField] int m_amountPerWave;
    [SerializeField] float m_rotationSpeed;
    [SerializeField] BulletGraphics m_bulletGraphics;
    [SerializeField] Rotation rotation;
    Transform m_parent;
    List<Bullet> m_bullets = new List<Bullet>();
    Bullet m_firstBullet;

    protected override void Start() {
        m_parent = this.transform;
        float angle = GetAngle();
        Vector2 direction = Vector2.down;
        Vector2 position;

        m_parent.eulerAngles = new Vector3(0, 0, 0);

        for (int i = 0; i < m_amountPerWave; i++) {
            position = (Vector2)m_parent.position + direction;
            m_bullets.Add(new Bullet(m_bulletGraphics.data, position, m_parent));
            m_parent.eulerAngles += new Vector3(0, 0, angle);
            if (m_firstBullet == null) {
                m_firstBullet = m_bullets[i];
            }
        }
    }

    public float GetAngle() {
        return 360 / m_amountPerWave;
    }

    private void CheckForDeath() {
        if (Vector2.Distance(m_parent.position, m_firstBullet.GetGraphics().transform.position) > Camera.main.scaledPixelHeight * 0.03f) {
            Destroy(m_parent.gameObject);
        }
    }

    protected override void Update() {
        m_parent.RotateAround(this.transform.position, Vector3.forward * (int)rotation, m_rotationSpeed * 10 * Time.deltaTime);
        for (int i = 0; i < m_bullets.Count; i++) {
            m_bullets[i].Update();
        }

        CheckForDeath();
    }
}

public enum Rotation {
    ClockWise = -1,
    CounterClockWise = 1,
    NONE = 0,
}
