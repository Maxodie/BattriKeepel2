using UnityEngine;
using Components;

[CreateAssetMenu(menuName = "Entities/Player", fileName = "Player")]
public class SO_PlayerData : ScriptableObject {
    public PlayerGraphics playerGraphics;
    public Hitbox hitBox;
    public SO_CharacterAttackData attackData;
}
