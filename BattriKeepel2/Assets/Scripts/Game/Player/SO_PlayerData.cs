using UnityEngine;
using Game.AttackSystem.Attacks;
using Game.AttackSystem.Bullet;

[CreateAssetMenu(menuName = "Entities/Player", fileName = "Player")]
public class SO_PlayerData : ScriptableObject {
    [Header("Default")]
    public string playerName;
    public string playerDesc;
    public Sprite playerVisual;

    [Header("Selection Menu")]
    public Animation selectionAnim;
    public Color nameColor;

    [Header("Logic")]
    public PlayerGraphics playerGraphics;

    public AttackSet attackSet;

    [Header("Distance Player Infos")]
    public BulletBehaviour bulletBehaviour;
    public BulletGraphics bulletGraphics;

    [Header("Camera Effects")]
    public float shakeAmount;
    public float shakeSpeed;

    [Header("Sound")]
    public AudioClip singleTapAttackSound;
    public AudioClip doubleTapAttackSound;
    public AudioClip shakeAttackSound;
}
