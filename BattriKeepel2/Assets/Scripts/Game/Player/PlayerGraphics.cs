using UnityEngine;
using Inputs;

public class PlayerGraphics : GameEntityGraphics {
    [SerializeField] Sprite m_playerSprite;
    [SerializeField] Transform m_playerTransform;
    public InputManager inputManager;
}
