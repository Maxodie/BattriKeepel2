using UnityEngine;
using UnityEngine.UI;

public class BulletGraphics : GameEntityGraphics
{
    [SerializeField] public Image sprite;
    [SerializeField] public SO_BulletData data;

    IGameEntity m_damageableEntity;

    void OnTriggerEnter2D(Collider2D col)
    {
        EntityGraphics entityGraphics = col.gameObject.GetComponent<EntityGraphics>();
        if(entityGraphics && entityGraphics.GetOwner() == m_damageableEntity)
        {
            entityGraphics.TakeDamage(data.damage);
        }
    }
}
