using UnityEngine;

public class BossLogger : Logger
{

}

namespace Components
{

public class BossMovement : Movement
{
    int m_currentLocation = 0;
    BossGraphicsEntity m_bossGraphics;
    SO_BossMovementData m_data;

    bool m_isMoving = false;
    Vector2 m_targetPosition;
    float m_positionThreashold = 0.2f;

    float m_waitTimer = 0.0f;

    public BossMovement(BossGraphicsEntity bossGraphics, SO_BossMovementData data)
    {
        m_bossGraphics = bossGraphics;
        m_currentLocation = 0;
        m_data = data;

        SetupNextLocationPoint();
    }

    public void Update()
    {
        if(!m_isMoving)
        {
            HandleWait();
            return;
        }

        m_targetPosition = m_bossGraphics.locationPoints[m_currentLocation];
        Vector2 lerpPos = Vector2.Lerp(m_bossGraphics.transform.position, m_targetPosition, Time.deltaTime * m_data.speed);

        //HandleMovement(lerpPos);
    }

    //public override void HandleMovement(Vector2 pos)
    //{
    //    m_bossGraphics.SetPosition(pos);

    //    m_isMoving = Vector2.Distance(m_bossGraphics.transform.position, m_targetPosition) > m_positionThreashold;
    //}

    public void HandleWait()
    {
        m_waitTimer += Time.deltaTime;

        if(m_waitTimer >= m_data.waitForNextPosDuration)
        {
            SetupNextLocationPoint();
            m_waitTimer = 0.0f;
        }
    }

    void SetupNextLocationPoint()
    {
        m_isMoving = true;
        m_currentLocation = GetNextRandomLocationID();
        Log.Info<BossLogger>("boss is ready to move to " + m_currentLocation);
    }

    int GetNextRandomLocationID()
    {
        if(m_bossGraphics.locationPoints.Length > 1)
        {
            int newLocationID = m_currentLocation;
            while(newLocationID == m_currentLocation)
            {
                newLocationID = Random.Range(0, m_bossGraphics.locationPoints.Length);
            }
            return newLocationID;
        }
        else
        {
            return 0;
        }
    }

        public override void HandleMovement()
        {
            throw new System.NotImplementedException();
        }
    }
}
