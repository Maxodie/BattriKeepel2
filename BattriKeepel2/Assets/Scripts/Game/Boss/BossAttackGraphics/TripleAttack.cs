using System.Collections.Generic;
using GameEntity;
using UnityEngine;

public class TripleAttack : BossAttackParent {
    [SerializeField] SO_TripleAttackData data;

    GameObject origin;
    Vector2 bossPosition;
    List<Bullet> m_bullets = new List<Bullet>();
    SO_TripleAttackData m_data;

    AttackGraphicsPool m_pool;
    int bulletCount;

    public override void Init(BossEntity boss, SO_BossAttackData data, AttackGraphicsPool graphicsPool, Player player)
    {
        origin = new GameObject();
        // origin.SetActive(true);
        origin.transform.position = boss.GetGraphics().transform.position;
        this.bossPosition = boss.GetGraphics().transform.position;
        m_pool = graphicsPool;
        m_data = (SO_TripleAttackData)data;

        float angle = Mathf.Atan2
            (player.position.y - bossPosition.y, player.position.x - bossPosition.x);

        angle = angle * Mathf.Rad2Deg;

        PlaceBullets();

        origin.transform.eulerAngles = new Vector3(angle, 0, 0);
    }

    private void PlaceBullets() {
        for (int i = -1; i < 2; i++) {
            float currentXEuleur = origin.transform.eulerAngles.x;

            origin.transform.eulerAngles = new Vector3(currentXEuleur + m_data.angle * i, 0, 0);
            m_bullets.Add(new Bullet(m_pool, m_data.bulletData, Vector3.down * 0.5f, origin.transform, true, Vector3.down, typeof(Player)));

            origin.transform.eulerAngles = new Vector3(currentXEuleur, 0, 0);
        }
    }

    public override void Update() {
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
            Clean();
        }
    }

    public override void Clean() {
        foreach(Bullet bullet in m_bullets)
        {
            if(bullet.GetGraphics() != null)
            {
                m_pool.StopBullet(bullet.GetGraphics());
            }
        }
        origin.gameObject.SetActive(false); // get out of this shit
    }
}
