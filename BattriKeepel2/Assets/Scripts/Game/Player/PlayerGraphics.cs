using UnityEngine;
using Inputs;

public class PlayerGraphics : GameEntityGraphics {
    [SerializeField] Sprite m_playerSprite;
    public Transform m_playerTransform;
    public InputManager inputManager;
}
