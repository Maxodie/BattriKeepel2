using UnityEngine;

[CreateAssetMenu(fileName = "bossAttack", menuName = "bossAttack")]
public class SO_BossAttackData : ScriptableObject
{
    public BossAttackGraphics bossAttackGraphics;
    public int intervalBeforeNextAttack;
}
