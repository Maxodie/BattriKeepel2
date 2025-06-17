using UnityEngine;
using UnityEngine.UI;

public class BulletGraphics : GameEntityGraphics
{
    [SerializeField] public Image sprite;
    [SerializeField] public SO_BulletData data;

    System.Type m_damageableType;

    public void Setup(System.Type damageable)
    {
        m_damageableType = damageable;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        EntityGraphics entityGraphics = col.gameObject.GetComponent<EntityGraphics>();
        if(entityGraphics && entityGraphics.GetOwner().GetType() == m_damageableType)
        {
            entityGraphics.TakeDamage(data.damage);
            ((Bullet)GetOwner()).Kill();
        }
    }
}
