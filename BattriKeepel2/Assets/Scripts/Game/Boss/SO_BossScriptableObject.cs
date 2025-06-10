using UnityEngine;

[CreateAssetMenu(fileName = "BossPaternScriptableObject", menuName = "Boss/BossPaternScriptableObject")]
public class SO_BossScriptableObject : ScriptableObject
{
    public BossGraphicsEntity bossGraphicsEntity;
    public SO_BossAttackData[] attackData;
    public SO_DialogData dialogData;
    public AudioClip damageSound;
    public float health;
}
