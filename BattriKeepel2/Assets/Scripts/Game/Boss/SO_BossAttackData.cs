using UnityEngine;

public class SO_BossAttackData : ScriptableObject
{
    public int intervalBeforeNextAttack;

    public virtual System.Type GetAttackType()
    {
        return typeof(SO_BossAttackData);
    }
}
