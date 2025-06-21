using UnityEngine;

[CreateAssetMenu(fileName = "bullet", menuName = "bullet")]
public class SO_BulletData : ScriptableObject {
    public int damage;
    public float speed;
    public BulletGraphics bulletGraphics;

    public Color color;
}
