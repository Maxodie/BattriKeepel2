using Game.AttackSystem.Attacks;
using UnityEngine;

public abstract class SO_EntityAttackData : ScriptableObject {
    public Attack primaryAttack;
}

[CreateAssetMenu(fileName = "CharacterAttackData", menuName = "Player/AttackData")]
public class SO_CharacterAttackData : SO_EntityAttackData {
    public Attack specialAttack;
    public Attack ultimate;
}
