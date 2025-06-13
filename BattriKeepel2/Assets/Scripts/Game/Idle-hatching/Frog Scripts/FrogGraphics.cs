using UnityEngine;

[System.Serializable]
public class FrogGraphics : GameEntityGraphics
{
    private Frog frogData;

    [SerializeField] private SpriteRenderer emptyFrogSprite;
    public Transform leapPosition;
    public Rigidbody2D rb;
    [SerializeField] SpriteRenderer m_spriteRenderer;
    [SerializeField] Sprite m_redSprite;
    [SerializeField] Sprite m_blueSprite;
    [SerializeField] Sprite m_greenSprite;
    [SerializeField] Material m_redMat;
    [SerializeField] Material m_greenMat;
    [SerializeField] Material m_blueMat;

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
                m_spriteRenderer.material = m_redMat;
                m_spriteRenderer.sprite = m_redSprite;
                break;
            case EN_FrogColors.GREEN:
                m_spriteRenderer.material = m_greenMat;
                m_spriteRenderer.sprite = m_greenSprite;
                break;
            case EN_FrogColors.BLUE:
                m_spriteRenderer.material = m_blueMat;
                m_spriteRenderer.sprite = m_blueSprite;
                break;
            default:
                Log.Error("No color found");
                break;
        }
    }
}
