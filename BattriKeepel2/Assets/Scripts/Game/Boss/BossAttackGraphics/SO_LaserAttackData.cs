using UnityEngine;

public class SO_LaserAttackData : ScriptableObject {
    public float m_timeToAppear;
    public float m_timeLasting;
    public LaserGraphics m_laserGraphics;
    public int m_spacePartitions;
    public int m_position; // from zero to m_spacePartitions - 1
    public bool m_followPlayer;
}
