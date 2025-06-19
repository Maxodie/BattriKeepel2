using UnityEngine;

[System.Serializable]
public class FrogGraphics : GameEntityGraphics
{
    private FrogDynamicData frogData;

    [SerializeField] private SpriteRenderer emptyFrogSprite;
    public Transform leapPosition;
    public Rigidbody2D rb;

    public void InitFrogGraphics(FrogDynamicData f)
    {
        SetFrogData(f);
        ComputeColor();
    }

    private void SetFrogData(FrogDynamicData fData)
    {
        frogData = fData;
    }

    private void ComputeColor()
    {
        emptyFrogSprite.sprite = FrogGenerator.Get().QueryVisual(frogData.m_rarity);
        emptyFrogSprite.material = FrogGenerator.Get().QueryMaterial(frogData.m_rarity);
    }
}
