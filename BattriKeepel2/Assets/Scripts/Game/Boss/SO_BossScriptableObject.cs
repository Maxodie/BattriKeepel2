using UnityEngine;

[CreateAssetMenu(fileName = "BossPaternScriptableObject", menuName = "Boss/BossPaternScriptableObject")]
public class SO_BossScriptableObject : ScriptableObject
{
    public BossGraphicsEntity bossGraphicsEntity;
    public SO_BossAttackPhase[] attackDataPhases;
    public SO_DialogData dialogData;
    public SO_DialogData nuisanceDialogData;
    public AudioClip damageSound;
    public float health;

    public float nuisanceLoopTime;
}
