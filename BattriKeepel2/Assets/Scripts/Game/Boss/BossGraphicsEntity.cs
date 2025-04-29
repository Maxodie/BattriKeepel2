using UnityEngine;
using UnityEngine.UI;
using Components;

public class BossGraphicsEntity : GameEntityGraphics
{
    [SerializeField] SpriteRenderer bossGraphics;
    [SerializeField] Image healthFill;

    [SerializeField] Transform[] transformPoints;
    [HideInInspector] public Vector2[] locationPoints;

    BossMovement m_movement;

    public void ComputeLocations()
    {
        locationPoints = new Vector2[transformPoints.Length];
        for(int i = 0; i < transformPoints.Length; i++)
        {
            locationPoints[i] = transformPoints[i].position;
        }
    }

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
