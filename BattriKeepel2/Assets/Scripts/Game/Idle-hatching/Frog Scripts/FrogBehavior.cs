using System.Collections;
using UnityEngine;

[System.Serializable]
public class FrogBehavior
{
    private MonoBehaviour m_MonoBehaviour; //Remplacer MonoBehaviour par Frog si besoin à un moment de la Frog

    [SerializeField] private Transform m_transform;
    [SerializeField] private Transform m_leapPosition;
    [SerializeField] private float m_leapCooldown;

    private Vector3 m_position;

    private bool m_isRotating;
    private bool m_isMoving;

    private Quaternion m_targetRotation;

    private float m_rotationTimer;
    private float m_leapTimer;

    public bool m_isRunning = false;
    FrogInteraction m_frogInteraction;
    Vector2 boundMin;
    Vector2 boundMax;

    public void InitFrogBehavior(FrogInteraction frogInteraction, FrogGraphics graphicsBehaviour, float leapCooldown)
    {
        m_MonoBehaviour = graphicsBehaviour;
        m_transform = graphicsBehaviour.transform;
        m_leapPosition = graphicsBehaviour.leapPosition;
        m_leapCooldown = leapCooldown;
        m_frogInteraction = frogInteraction;
        StartBehavior();

    }

    private void StartBehavior()
    {
        Vector3 newDir = SearchNewDirection();
        boundMin = GraphicsManager.Get().BoundsMin(Camera.main);
        boundMax = GraphicsManager.Get().BoundsMax(Camera.main);
        m_MonoBehaviour.StartCoroutine(WaitBetweenLeaps(m_leapCooldown));
        m_isRunning = true;
    }

    public void UpdateBehavior()
    {
        if(!m_isRunning || !m_frogInteraction.canInputInteraction)
        {
            return;
        }

        CheckCollision();

        if (m_isRotating)
        {
            ExecuteRotation();
        }
        else if (m_isMoving)
        {
            ExecuteLeap();
        }
    }

    void CheckCollision()
    {
        if(m_transform.position.x <= boundMin.x)
        {
            m_transform.position = new Vector2(boundMin.x + 0.5f, m_transform.position.y);
        }

        if(m_transform.position.x >= boundMax.x)
        {
            m_transform.position = new Vector2(boundMax.x - 0.5f, m_transform.position.y);
        }

        if(m_transform.position.y <= boundMin.y)
        {
            m_transform.position = new Vector2(m_transform.position.x, boundMin.y + 0.5f);
        }

        if(m_transform.position.y >= boundMax.y)
        {
            m_transform.position = new Vector2(m_transform.position.x, boundMax.y - 0.5f);
        }
    }

    Vector3 SearchNewDirection()
    {
        Vector3 newDirection = Random.insideUnitCircle.normalized;
        float targetAngle = Mathf.Atan2(newDirection.x, newDirection.y) * Mathf.Rad2Deg; //merci Loutre
        m_targetRotation = Quaternion.Euler(0, 0, -targetAngle);
        m_position = m_leapPosition.position - m_transform.position;
        m_isRotating = true;
        return newDirection;
    }

    IEnumerator WaitBetweenLeaps(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Vector3 newDir = SearchNewDirection();
        m_isRotating = true;
        m_MonoBehaviour.StartCoroutine(WaitBetweenLeaps(m_leapCooldown));
    }

    void ExecuteRotation()
    {
        m_transform.rotation = Quaternion.Lerp(m_transform.rotation, m_targetRotation, Time.deltaTime);
        m_rotationTimer += Time.deltaTime;
        if (m_rotationTimer >= 1)
        {
            m_rotationTimer = 0;
            m_isRotating = false;
            m_isMoving = true;
        }
    }

    void ExecuteLeap()
    {
        m_transform.position = Vector3.Lerp(m_transform.position, m_transform.position + m_position, Time.deltaTime);
        m_leapTimer += Time.deltaTime;
        if (m_leapTimer >= 1)
        {
            m_leapTimer = 0;
            m_isMoving = false;
        }
    }
}
