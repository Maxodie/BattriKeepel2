using UnityEngine;

public class FrogBuild : IGameEntity
{
    EN_BuildType m_BuildType;

    private int m_TimeToEXPMultiplier = 2;

    private float m_TimeSpentInBuild;

    private bool m_IsFrogWorking;

    private Frog m_CurrentFrogData;
    FrogFarm m_currentFarm;
    FrogBuildGraphics m_frogbuildGraphics;

    public FrogBuild(EN_BuildType buildType, FrogBuildGraphics graphicsPrefab, FrogFarm currentFarm, Transform spawnPoint)
    {
        m_BuildType = buildType;
        m_currentFarm = currentFarm;
        m_frogbuildGraphics = GraphicsManager.Get().GenerateVisualInfos<FrogBuildGraphics>(graphicsPrefab, spawnPoint, this);
        m_frogbuildGraphics.SetActiveProgress(false);
        m_TimeSpentInBuild = 0;
    }

    public void Update()
    {
        if (m_IsFrogWorking)
        {
            m_TimeSpentInBuild += Time.deltaTime;
            m_frogbuildGraphics.SetFrogFill(m_TimeSpentInBuild / (float)m_frogbuildGraphics.buildDuration);

            if(m_frogbuildGraphics.buildDuration <= m_TimeSpentInBuild)
            {
                m_TimeSpentInBuild = 0;
                m_IsFrogWorking = false;
                CancelWorking();
            }
        }

    }

    public void StartWorking(Frog frog)
    {
        m_IsFrogWorking = true;
        m_CurrentFrogData = frog;
        m_CurrentFrogData.SetActive(false);
        m_CurrentFrogData.SetPosition(m_frogbuildGraphics.transform.position);
        m_frogbuildGraphics.SetActiveProgress(true);
    }

    public void CancelWorking()
    {
        m_IsFrogWorking = false;
        AddExpToFarm();
        m_CurrentFrogData.SetActive(true);
        m_CurrentFrogData.SetPosition(Vector2.zero);
        m_TimeSpentInBuild = 0;

        m_frogbuildGraphics.SetActiveProgress(false);
        m_CurrentFrogData = null;
    }

    public void AddExpToFarm()
    {
        switch (m_BuildType)
        {
            case EN_BuildType.RUN_BUILD:
                m_currentFarm.AddXp((int)(m_CurrentFrogData.m_frogDynamicData.m_RunLevel * m_TimeToEXPMultiplier));
                break;
            case EN_BuildType.FLY_BUILD:
                m_currentFarm.AddXp((int)(m_CurrentFrogData.m_frogDynamicData.m_FlyLevel * m_TimeToEXPMultiplier));
                break;
            case EN_BuildType.SWIM_BUILD:
                m_currentFarm.AddXp((int)(m_CurrentFrogData.m_frogDynamicData.m_SwimLevel * m_TimeToEXPMultiplier));
                break;
            default:
                Log.Error("Cannot find the correct type");
                break;
        }
    }
}
