using System.Collections.Generic;
using Game.AttackSystem.Bullet;
using UnityEngine;

public class SpiralAttackGraphics : BossAttackGraphics {
    [SerializeField] int m_waveAmount;
    [SerializeField] int m_amountPerWave;
    [SerializeField] float m_rotationSpeed;
    [SerializeField] BulletGraphics m_bulletGraphics;
    Transform m_parent;
    List<Bullet> m_bullets = new List<Bullet>();

    protected override void Start() {
        m_parent = this.transform;
        float angle = GetAngle();
        Vector2 direction = Vector3.down;
        Vector2 position;

        for (int i = 0; i < m_amountPerWave; i++) {
            position = (Vector2)m_parent.position + direction;
            m_bullets.Add(new Bullet(m_bulletGraphics.data, position, direction));
            direction = GetNewDirection(direction, angle);
        }
    }

    public Vector2 GetNewDirection(Vector3 position, float angle) {
        double angleRadians = angle * Mathf.PI / 180;
        double cosTheta = Mathf.Cos((float)angleRadians);
        double sinTheta = Mathf.Sin((float)angleRadians);

        double x = position.x * cosTheta - position.y * sinTheta;
        double y = position.x * sinTheta - position.y * cosTheta;

        return new Vector3((float)x, (float)y, 0);
    }

    public float GetAngle() {
        return 360 / m_amountPerWave;
    }

    protected override void Update() {
        m_parent.RotateAround(this.transform.position, Vector3.forward, m_rotationSpeed * 10 * Time.deltaTime);;
    }
}
