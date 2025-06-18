using System.Collections.Generic;
using UnityEngine;

public class SpiralAttack : BossAttackParent {
    [SerializeField] int m_amountPerWave;
    [SerializeField] float m_rotationSpeed;
    [SerializeField] SO_BulletData m_bulletData;
    [SerializeField] Rotation rotation;
    Transform m_parent;
    List<Bullet> m_bullets = new List<Bullet>();

    public void Start() {
        m_parent = transform;
        float angle = GetAngle();
        Vector2 direction = Vector2.down;
        Vector2 position;

        m_parent.eulerAngles = new Vector3(0, 0, 0);

        for (int i = 0; i < m_amountPerWave; i++)
        {
            position = (Vector2)m_parent.position + direction;
            m_bullets.Add(new Bullet(m_bulletData, position, m_parent, true, Vector3.down, typeof(GameEntity.Player)));
            m_parent.eulerAngles += new Vector3(0, 0, angle);
        }
    }

    public float GetAngle() {
        return 360 / m_amountPerWave;
    }

    public void Update() {
        m_parent.RotateAround(m_parent.position, Vector3.forward * (int)rotation, m_rotationSpeed * 10 * Time.deltaTime);
        for (int i = 0; i < m_bullets.Count; i++) {

            if (m_bullets[i].IsDead()) {
                Object.Destroy(m_parent.gameObject);
                m_bullets.RemoveAt(i);
                i--;
                continue;
            }

            m_bullets[i].Update();
        }
    }
}

public enum Rotation {
    ClockWise = -1,
    CounterClockWise = 1,
    NONE = 0,
}
