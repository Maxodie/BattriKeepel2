using UnityEngine;
using TMPro;

[System.Serializable]
public class FrogGraphics : GameEntityGraphics
{
    private FrogDynamicData frogData;

    [SerializeField] private SpriteRenderer emptyFrogSprite;
    [SerializeField] private TMP_Text swimlevelText;
    [SerializeField] private TMP_Text runlevelText;
    [SerializeField] private TMP_Text flylevelText;
    public Transform leapPosition;
    public Rigidbody2D rb;

    public void InitFrogGraphics(FrogDynamicData f)
    {
        SetFrogData(f);
        ComputeColor();
    }

    public void OnChangeLevel()
    {
        swimlevelText.text = $"Swim LVL: {frogData.m_SwimLevel}";
        runlevelText.text = $"Run LVL: {frogData.m_RunLevel}";
        flylevelText.text = $"Fly LVL: {frogData.m_FlyLevel}";
    }

    private void SetFrogData(FrogDynamicData fData)
    {
        frogData = fData;
    }

    public Frog GetFrog()
    {
        return GetOwner<Frog>();
    }

    private void ComputeColor()
    {
        emptyFrogSprite.sprite = FrogGenerator.Get().QueryVisual(frogData.m_rarity);
        emptyFrogSprite.material = FrogGenerator.Get().QueryMaterial(frogData.m_rarity);
    }
}
