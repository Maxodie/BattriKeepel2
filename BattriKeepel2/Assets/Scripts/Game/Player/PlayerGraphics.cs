using DG.Tweening;
using GameEntity;
using UnityEngine;
using Inputs;
using UnityEngine.InputSystem;

public class PlayerGraphics : EntityGraphics {
    [SerializeField] Sprite m_playerSprite;
    [SerializeField] SpriteRenderer m_playerSpriteRenderer;
    [SerializeField] Material m_playerMat;
    public Transform m_playerTransform;
    public InputManager inputManager;
    public Rigidbody2D rb;
    
    private DOTween tween ;
    private Sequence sequence;

    private Vector2 scale;

    private Player player;

    public void Init()
    {
        m_playerSpriteRenderer.sprite = m_playerSprite;
        m_playerSpriteRenderer.material = m_playerMat;

        sequence = DOTween.Sequence();
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

    public bool GetInvincibility()
    {
        return player.GetInvincibility();
    }

    public void SetTransparency(bool isInvincibility)
    {
        m_playerSpriteRenderer.color = isInvincibility ? new Color(m_playerSpriteRenderer.color.r, m_playerSpriteRenderer.color.g, m_playerSpriteRenderer.color.b, 0.5f) 
                                                        : new Color(m_playerSpriteRenderer.color.r, m_playerSpriteRenderer.color.g, m_playerSpriteRenderer.color.b, 1);
    }

    public void SetPlayer(Player playerToSet)
    {
        player = playerToSet;
    }

    public void AttackAnim(float stretchStrength)
    {
        switch (stretchStrength)
        {
            case 1 : 
                scale = new Vector2(0.45f, 0.45f);
                sequence.Append(transform.DOScale(new Vector2(0.30f, 0.30f), 0.1f));
                sequence.Append(transform.DOScale(new Vector2(0.45f, 0.45f), 0.1f));
                break;
            case 2 :
                break;
            default :
                break;
        }
    }
}
