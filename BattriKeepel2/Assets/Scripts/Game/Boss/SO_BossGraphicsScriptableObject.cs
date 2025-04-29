using UnityEngine;
using Components;

[CreateAssetMenu(fileName = "BossPaternScriptableObject", menuName = "Boss/BossPaternScriptableObject")]
public class SO_BossGraphicsScriptableObject : ScriptableObject
{
    public BossGraphicsEntity bossGraphicsEntity;
    public Hitbox hitbox;
    public float speed;
}
