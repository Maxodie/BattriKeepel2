using UnityEngine;

[CreateAssetMenu(fileName = "bossTripleAttack", menuName = "boss/bossTripleAttack")]
public class SO_TripleAttackData : SO_BossAttackData {
    public SO_BulletData bulletData;
    public float angle;

    public override System.Type GetAttackType()
    {
        return typeof(TripleAttack);
    }
}
