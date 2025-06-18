using UnityEngine;

[System.Serializable]
public class FrogGraphics : GameEntityGraphics
{
    private Frog frogData;

    [SerializeField] private SpriteRenderer emptyFrogSprite;
    public Transform leapPosition;
    public Rigidbody2D rb;

    public void InitFrogGraphics(Frog f)
    {
        SetFrogData(f);
        ComputeColor();
    }

    private void SetFrogData(Frog fData)
    {
        frogData = fData;
    }

    private void ComputeColor()
    {
        switch (frogData.m_Color)
        {
            case EN_FrogColors.RED:
                emptyFrogSprite.color = Color.red;
                break;
            case EN_FrogColors.GREEN:
                emptyFrogSprite.color = Color.green;
                break;
            case EN_FrogColors.BLUE:
                emptyFrogSprite.color = Color.blue;
                break;
            default:
                Log.Error("No color found");
                break;
        }
    }
}
