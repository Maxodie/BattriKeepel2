using System;
using GameEntity;
using UnityEngine;
using Inputs;
using UnityEngine.InputSystem;

public class PlayerGraphics : GameEntityGraphics {
    [SerializeField] Sprite m_playerSprite;
    [SerializeField] SpriteRenderer m_playerSpriteRenderer;
    [SerializeField] Material m_playerMat;
    public Transform m_playerTransform;
    public InputManager inputManager;
    public Rigidbody2D rb;

    private Player player;

    public void Init()
    {
        m_playerSpriteRenderer.sprite = m_playerSprite;
        m_playerSpriteRenderer.material = m_playerMat;
    }

    private void Update()
    {
        if (Keyboard.current[Key.A].wasPressedThisFrame)
        {
            player.LaunchUltimate();
        }
    }

    public Player GetPlayer()
    {
        return player;
    }

    public void SetPlayer(Player playerToSet)
    {
        player = playerToSet;
    }
}
