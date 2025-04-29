using UnityEngine;
using UnityEngine.UI;

public class BossGraphicsEntity : GameEntityGraphics
{
    [SerializeField] SpriteRenderer bossGraphics;
    [SerializeField] Image healthFill;

    public void SetVisual(Sprite visual)
    {
        bossGraphics.sprite = visual;
    }

    public void SetHP(float percentage)
    {
        healthFill.fillAmount = percentage;
    }
}
