using UnityEngine;

[System.Serializable]
public class FrogInteraction
{
    private Rigidbody2D rb;
    private Vector2 newPos = new Vector2();
    private Vector2 dirtyPos = Vector2.zero;
    public Vector2 vel;
    private UnityEngine.InputSystem.TouchPhase m_isPressed;
    private FrogBehavior frogBehavior;

    public bool isInInputInteraction = false;
    public bool canInputInteraction = true;

    public FrogInteraction(Rigidbody2D rb, FrogBehavior frogBehavior)
    {
        this.rb = rb;
        this.frogBehavior = frogBehavior;
    }

    public void OnPosition(Vector2 position)
    {
        newPos = Camera.main.ScreenToWorldPoint(position);

        if (m_isPressed == UnityEngine.InputSystem.TouchPhase.Began)
        {
            dirtyPos = newPos;
            vel = Vector2.zero;
        }
    }

    public void OnPress(UnityEngine.InputSystem.TouchPhase state)
    {
        m_isPressed = state;
    }

    public bool HandleMovement()
    {
        if (!canInputInteraction || m_isPressed == UnityEngine.InputSystem.TouchPhase.Began
                || m_isPressed == UnityEngine.InputSystem.TouchPhase.Ended
                || m_isPressed == UnityEngine.InputSystem.TouchPhase.None ||
            (newPos.x <= rb.position.x - 1 || newPos.x >= rb.position.x + 1 ||
            newPos.y <= rb.position.y - 1 || newPos.y >= rb.position.y + 1))
        {
            rb.linearVelocity = Vector2.zero;
            frogBehavior.m_isRunning = true;
            isInInputInteraction = false;
            return false;
        }

        frogBehavior.m_isRunning = false;
        isInInputInteraction = true;

        vel = (newPos - dirtyPos) / Time.deltaTime;
        dirtyPos = newPos;

        rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, vel, .7f);
        return true;
    }

    public bool IsScreenPressed()
    {
        if (m_isPressed != UnityEngine.InputSystem.TouchPhase.None && m_isPressed != UnityEngine.InputSystem.TouchPhase.Ended)
        {
            return true;
        }
        return false;
    }
}
