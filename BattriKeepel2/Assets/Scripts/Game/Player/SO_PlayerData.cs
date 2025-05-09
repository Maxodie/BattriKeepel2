using UnityEngine;
using Components;
using Game.AttackSystem.Attacks;
using Game.AttackSystem.Bullet;

[CreateAssetMenu(menuName = "Entities/Player", fileName = "Player")]
public class SO_PlayerData : ScriptableObject {
    public PlayerGraphics playerGraphics;
    public Hitbox hitBox;
    
    public AttackSet attackSet;
    
    [Header("Distance Player Infos")]
    public BulletBehaviour bulletBehaviour;
    public BulletGraphics bulletGraphics;

    [Header("Camera Effects")]
    public float shakeAmount;
    public float shakeSpeed;
}
