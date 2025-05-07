using UnityEngine;
using Inputs;

public class PlayerGraphics : GameEntityGraphics {
    [SerializeField] Sprite m_playerSprite;
    [SerializeField] SpriteRenderer m_playerSpriteRenderer;
    [SerializeField] Material m_playerMat;
    public Transform m_playerTransform;
    public InputManager inputManager;

    public void Init()
    {
        m_playerSpriteRenderer.sprite = m_playerSprite;
        m_playerSpriteRenderer.material = m_playerMat;
    }
}
