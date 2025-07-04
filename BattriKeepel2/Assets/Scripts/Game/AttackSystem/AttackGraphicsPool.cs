using UnityEngine;
using System.Collections.Generic;

public class AttackGraphicsPool : IGameEntity
{
    List<BulletGraphics> bullets = new();
    public AttackGraphicsPool(Transform bulletPoolParent)
    {
        int childsCount = bulletPoolParent.childCount;

        int i = 0;
        foreach(Transform child in bulletPoolParent)
        {
            BulletGraphics bullet = child.GetComponent<BulletGraphics>();
            bullets.Add(bullet);
            i++;
        }
    }

    void ClearBullets()
    {
        for(int i = 0; i < bullets.Count; i++)
        {
            if(bullets[i] == null)
            {
                bullets.RemoveAt(i);
                i--;
            }
        }
    }

    public BulletGraphics GetBulletGraphics(Vector2 position, Quaternion rotation, IGameEntity entity)
    {
        ClearBullets();

        foreach(BulletGraphics bullet in bullets)
        {
            if(bullet != null)
            {
                if(!bullet.gameObject.activeSelf)
                {
                    bullet.transform.SetParent(null);
                    bullet.transform.position = position;
                    bullet.transform.rotation = rotation;
                    bullet.StartPool();
                    return bullet;
                }
            }
        }

        BulletGraphics newBullet = GraphicsManager.Get().GenerateVisualInfos<BulletGraphics>(bullets[0], position, rotation, entity, false);
        bullets.Add(newBullet);
        newBullet.StartPool();
        return newBullet;
    }

    public BulletGraphics GetBulletGraphics(Transform parent, IGameEntity entity, bool isChild)
    {
        ClearBullets();

        foreach(BulletGraphics bullet in bullets)
        {
            if(!bullet.gameObject.activeSelf)
            {
                if(isChild)
                {
                    bullet.transform.SetParent(parent);
                    bullet.transform.position = Vector3.zero;
                    bullet.transform.rotation = Quaternion.identity;
                }
                else
                {
                    bullet.transform.SetParent(null);
                    bullet.transform.position = parent.position;
                    bullet.transform.rotation = parent.rotation;
                }
                bullet.StartPool();
                return bullet;
            }
        }

        BulletGraphics newBullet = GraphicsManager.Get().GenerateVisualInfos<BulletGraphics>(bullets[0], parent, entity, isChild);
        bullets.Add(newBullet);
        newBullet.StartPool();
        return newBullet;
    }

    public void StopBullet(BulletGraphics bulletToStore)
    {
        bulletToStore.EndPool();
    }
}
