/// <summary>
///You MUST call the Init methode WITH parameters,
/// </summary>
public class GameEntityGraphics : GameEntityMonoBehaviour
{
    IGameEntity m_owner;

    /// <summary>
    ///Bah ça init quoi, what DID(c'est une macro) you expect
    /// </summary>
    public void Init(IGameEntity owner)
    {
        m_owner = owner;
    }

    /// <summary>
    ///Can be NULL,,, a bon entendeure
    /// </summary>
    public IGameEntity GetOwner()
    {
        return m_owner;
    }
}
