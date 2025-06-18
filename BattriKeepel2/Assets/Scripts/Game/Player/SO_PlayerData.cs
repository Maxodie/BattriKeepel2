using UnityEngine;
using Game.AttackSystem.Attacks;

[CreateAssetMenu(menuName = "Entities/Player", fileName = "Player")]
public class SO_PlayerData : ScriptableObject {
    [Header("Default")]
    public string playerName;
    public string playerDesc;
    public Sprite playerVisual;
    public int maxHealth;

    [Header("Selection Menu")]
    public Animation selectionAnim;
    public Color nameColor;

    [Header("Logic")]
    public PlayerGraphics playerGraphics;

    public AttackSet attackSet;

    [Header("Bullet Datas")] 
    public SO_BulletData attackBulletData;
    public SO_BulletData abilityBulletData;
    public SO_BulletData ultimateBulletData;

    [Header("Camera Effects")]
    public float shakeAmount;
    public float shakeSpeed;

    [Header("Sound")]
    public AudioClip singleTapAttackSound;
    public AudioClip doubleTapAttackSound;
    public AudioClip shakeAttackSound;
}
