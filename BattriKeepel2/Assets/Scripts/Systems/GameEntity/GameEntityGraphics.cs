using UnityEngine;

/// <summary>
///You MUST call the Init methode WITH parameters,
/// </summary>
public class GameEntityGraphics : GameEntityMonoBehaviour
{
    [SerializeField] SO_GraphicsData m_data;

    IGameEntity m_owner;
    SpriteRenderer m_spriteRenderer;

    /// <summary>
    ///Bah ça init quoi, what DID(c'est une macro) you expect
    /// </summary>
    public void Init(IGameEntity owner)
    {
        m_owner = owner;
        m_spriteRenderer.sprite = m_data.sprite;
    }

    /// <summary>
    ///Can be NULL,,, a bon entendeure
    /// </summary>
    public IGameEntity GetOwner()
    {
        return m_owner;
    }
}
