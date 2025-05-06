using UnityEngine;
using Components;

[CreateAssetMenu(menuName = "Entities/Player", fileName = "Player")]
public class SO_PlayerData : ScriptableObject {
    public PlayerGraphics playerGraphics;
    public Hitbox hitBox;

    [Header("Camera Effects")]
    public float shakeAmount;
    public float shakeSpeed;
}
