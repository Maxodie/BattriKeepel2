using UnityEngine;

namespace Components
{

public class BossMovement : Movement
{
    int m_currentLocation = 0;
    BossGraphicsEntity m_bossGraphics;
    float m_speed = 0;

    public BossMovement(BossGraphicsEntity bossGraphics, float speed)
    {
        m_bossGraphics = bossGraphics;
        m_currentLocation = 0;
        m_speed = speed;
    }

    public void Update()
    {
        Vector2 nextLocation = m_bossGraphics.locationPoints[m_currentLocation].position;
        Vector2 lerpPos = Vector2.Lerp(m_bossGraphics.transform.position, nextLocation, Time.deltaTime * m_speed);

        HandleMovement(lerpPos);
    }

    public override void HandleMovement(Vector2 pos)
    {
        m_bossGraphics.SetPosition(pos);
    }

    void SetupNextLocationPoint()
    {
        m_currentLocation = GetNextRandomLocationID();
    }

    int GetNextRandomLocationID()
    {
        if(m_bossGraphics.locationPoints.Length > 1)
        {
            int newLocationID = m_currentLocation;
            while(newLocationID == m_currentLocation)
            {
                Random.Range(0, m_bossGraphics.locationPoints.Length);
            }
            return newLocationID;
        }
        else
        {
            return 0;
        }
    }
}

}
