using System.Collections.Generic;
using UnityEngine;

public class SpiralAttack : BossAttackParent {
    SO_SpiralAttackData data;
    Transform m_parent;
    List<Bullet> m_bullets = new List<Bullet>();
    int bulletCount = 0;

    AttackGraphicsPool m_bulletPool;

    public SpiralAttack()
    {
        m_parent = new GameObject().transform;
    }

    public override void Init(BossEntity boss, SO_BossAttackData data, AttackGraphicsPool graphicsPool, GameEntity.Player player) {
        this.data = (SO_SpiralAttackData)data;
        float angle = GetAngle();
        Vector2 direction = Vector2.down;
        Vector2 position;
        m_bulletPool = graphicsPool;

        m_parent.position = boss.GetGraphics().transform.position;
        m_parent.eulerAngles = new Vector3(0, 0, 0);

        for (int i = 0; i < this.data.m_amountPerWave; i++)
        {
            position = (Vector2)m_parent.position + direction;
            m_bullets.Add(new Bullet(graphicsPool, this.data.m_bulletData, position, m_parent, true, Vector3.down, typeof(GameEntity.Player)));
            m_parent.eulerAngles += new Vector3(0, 0, angle);
        }

        bulletCount = m_bullets.Count;
    }

    public float GetAngle() {
        return 360 / data.m_amountPerWave;
    }

    public override void Update() {
        m_parent.RotateAround(m_parent.position, Vector3.forward * (int)data.rotation, data.m_rotationSpeed * 10 * Time.deltaTime);
        bulletCount = m_bullets.Count;
        for (int i = 0; i < m_bullets.Count; i++) {

            if (m_bullets[i].IsDead()) {
                bulletCount--;
                continue;
            }

            m_bullets[i].Update();
        }

        if(bulletCount == 0)
        {
            Destroy();
        }
    }

    public void Destroy()
    {
        Clean();
        isActive = false;
    }

    public override void Clean()
    {
        foreach(Bullet bullet in m_bullets)
        {
            m_bulletPool.StopBullet(bullet.GetGraphics());
        }
        Object.Destroy(m_parent.gameObject); // get out of this shit
    }
}

public enum Rotation {
    ClockWise = -1,
    CounterClockWise = 1,
    NONE = 0,
}
