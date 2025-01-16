using System.Collections;
using UnityEngine;

public class FrogBehavior : MonoBehaviour
{
    [SerializeField] private Transform m_transform;
    [SerializeField] private Transform m_LeapPosition;

    private Vector3 m_position;

    private float m_waitTime;

    private bool m_isRotating;
    private bool m_isMoving;

    private Quaternion m_targetRotation;

    private void Start()
    {
        Vector3 newDir = SearchNewDirection();
        ExecuteLeap(newDir);
        StartCoroutine(WaitBetweenLeaps(5f));
    }

    private void Update()
    {
        if (m_isRotating)
        {
            m_transform.rotation = Quaternion.Lerp(m_transform.rotation, m_targetRotation, Time.deltaTime);
            if(m_transform.rotation == m_targetRotation)
            {
                m_isRotating = false;
                m_isMoving = true;
            }
        }
        else if (m_isMoving) 
        {
            m_transform.position = Vector3.Lerp(m_transform.position, m_LeapPosition.position, Time.deltaTime);

        }
    }

    Vector3 SearchNewDirection()
    {
        Vector3 newDirection = Random.insideUnitCircle.normalized;
        float targetAngle = Mathf.Atan2(newDirection.x, newDirection.y) * Mathf.Rad2Deg; //merci Loutre
        m_targetRotation = Quaternion.Euler(0, 0, -targetAngle);
        //m_transform.rotation = Quaternion.Euler(0, 0, -targetAngle);
        m_isRotating = true;
        return newDirection;
    }

    void ExecuteLeap(Vector2 direction)
    {
        //m_transform.position = Vector3.Lerp(m_transform.position, m_LeapPosition.position, Time.deltaTime);
    }

    IEnumerator WaitBetweenLeaps(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Vector3 newDir = SearchNewDirection();
        ExecuteLeap(newDir);
        StartCoroutine(WaitBetweenLeaps(1f));
    }
}
