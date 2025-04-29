using UnityEngine;

[CreateAssetMenu(fileName = "BossMovementData", menuName = "Boss/BossMovementData")]
public class SO_BossMovementData : ScriptableObject
{
    public float speed;
    public float waitForNextPosDuration = 2.0f;
}
