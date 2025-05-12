using UnityEngine;
using Components;

[CreateAssetMenu(fileName = "BossPaternScriptableObject", menuName = "Boss/BossPaternScriptableObject")]
public class SO_BossScriptableObject : ScriptableObject
{
    public BossGraphicsEntity bossGraphicsEntity;
    public SO_BossMovementData movementData;
    public SO_DialogData dialogData;
    public float health;
}
