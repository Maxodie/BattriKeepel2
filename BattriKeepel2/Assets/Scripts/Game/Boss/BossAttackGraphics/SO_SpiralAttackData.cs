using UnityEngine;

[CreateAssetMenu(fileName = "bossSpiralAttack", menuName = "boss/bossSpiralAttack")]
public class SO_SpiralAttackData : SO_BossAttackData {
    public int m_amountPerWave;
    public float m_rotationSpeed;
    public SO_BulletData m_bulletData;
    public Rotation rotation;

    public override System.Type GetAttackType()
    {
        return typeof(SpiralAttack);
    }
}
