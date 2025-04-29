using UnityEngine;
using UnityEngine.UI;
using Components;

public class BossGraphicsEntity : GameEntityGraphics
{
    [SerializeField] SpriteRenderer bossGraphics;
    [SerializeField] Image healthFill;

    public Transform[] locationPoints;

    BossMovement m_movement;

    public void SetVisual(Sprite visual)
    {
        bossGraphics.sprite = visual;
    }

    public void SetHP(float percentage)
    {
        healthFill.fillAmount = percentage;
    }

    public void SetPosition(Vector2 newPos)
    {
        transform.position = newPos;
    }
}
