using UnityEngine;

[CreateAssetMenu(fileName = "bossLaserAttack", menuName = "boss/bossLaserAttack")]
public class SO_LaserAttackData : SO_BossAttackData {
    public float m_timeToAppear;
    public float m_timeLasting;
    public LaserGraphics m_laserGraphics;
    public int m_spacePartitions;
    public int m_position; // from zero to m_spacePartitions - 1
    public float m_laserScale;
    public float m_followTime;
    public bool m_followPlayer;

    public override System.Type GetAttackType()
    {
        return typeof(LaserAttack);
    }
}
