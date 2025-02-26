using System.Collections;
using UnityEngine;

public class FrogBehavior : MonoBehaviour
{
    [SerializeField] private Transform m_transform;
    [SerializeField] private Transform m_leapPosition;
    [SerializeField] private float m_leapCooldown;

    private Vector3 m_position;

    private bool m_isRotating;
    private bool m_isMoving;

    private Quaternion m_targetRotation;

    private float m_rotationTimer;
    private float m_leapTimer;

    private void Start()
    {
        Vector3 newDir = SearchNewDirection();
        StartCoroutine(WaitBetweenLeaps(m_leapCooldown));
    }

    private void Update()
    {
        if (m_isRotating)
        {
            ExecuteRotation();
        }
        else if (m_isMoving) 
        {
            ExecuteLeap();
        }
    }

    Vector3 SearchNewDirection()
    {
        Vector3 newDirection = Random.insideUnitCircle.normalized;
        float targetAngle = Mathf.Atan2(newDirection.x, newDirection.y) * Mathf.Rad2Deg; //merci Loutre
        m_targetRotation = Quaternion.Euler(0, 0, -targetAngle);
        m_isRotating = true;
        Log.Success("Direction found !");
        return newDirection;
    }

    IEnumerator WaitBetweenLeaps(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Vector3 newDir = SearchNewDirection();
        m_isRotating = true;
        StartCoroutine(WaitBetweenLeaps(m_leapCooldown));
    }

    void ExecuteRotation()
    {
        m_transform.rotation = Quaternion.Lerp(m_transform.rotation, m_targetRotation, Time.deltaTime);
        m_rotationTimer += Time.deltaTime;
        if (m_rotationTimer >= 1)
        {
            m_rotationTimer = 0;
            Log.Success("Rotation finished, execute leap");
            m_isRotating = false;
            m_isMoving = true;
        }
    }

    void ExecuteLeap()
    {
        m_transform.position = Vector3.Lerp(m_transform.position, m_leapPosition.position, Time.deltaTime);
        m_leapTimer += Time.deltaTime;
        if (m_leapTimer >= 1)
        {
            m_leapTimer = 0;
            Log.Success("Leap finished, search for new direction");
            m_isMoving = false;
        }
    }
}
