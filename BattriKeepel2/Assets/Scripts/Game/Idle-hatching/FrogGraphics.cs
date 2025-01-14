using UnityEngine;

public class FrogGraphics : MonoBehaviour
{
    [SerializeField] public Frog frogData;

    [SerializeField] private SpriteRenderer emptyFrogSprite;

    public void SetFrogData(Frog fData)
    {
        frogData = fData;
    }

    public void ComputeColor()
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
