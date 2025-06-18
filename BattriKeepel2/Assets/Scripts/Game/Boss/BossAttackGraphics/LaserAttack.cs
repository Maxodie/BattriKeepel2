using UnityEngine;

public class LaserAttack : BossAttackParent {
    [SerializeField] float m_timeToAppear;
    [SerializeField] float m_timeLasting;
    [SerializeField] SpawnDir m_direction;
    [SerializeField] LaserGraphics m_laserGraphics;

    float appearTime;
    float lastingTime;

    private void Start() {
        m_laserGraphics = GraphicsManager.Get().GenerateVisualInfos<LaserGraphics>
            (m_laserGraphics, m_direction, Vector3.zero, Quaternion.identity, this, true);
        appearTime = m_timeToAppear;
        lastingTime = m_timeLasting;
    }

    private void Update() {
        if (m_timeToAppear > 0) {
            m_timeToAppear -= Time.deltaTime;
            m_laserGraphics.SetFillInSize(LerpTime(m_timeToAppear, appearTime));
        } else if (m_timeLasting > 0) {
            m_laserGraphics.TriggerLaser(); // change that so that it only triggers once
            m_timeLasting -= Time.deltaTime;
        } else if (0 >= m_timeLasting) {
            Destroy(m_laserGraphics); // doesn't work idk why
        }
    }

    private float LerpTime(float currentTime, float maxTime) {
        if (0 > currentTime) {
            return 1;
        }
        return 1 - currentTime / maxTime;
    }
}
