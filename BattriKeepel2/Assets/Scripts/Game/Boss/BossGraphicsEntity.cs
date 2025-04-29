using UnityEngine;

public class BossGraphicsEntity : GameEntityGraphics
{
    [SerializeField] SpriteRenderer bossGraphics;

    public void SetVisual(Sprite visual)
    {
        bossGraphics.sprite = visual;
    }
}
