using UnityEngine;

public class Build : MonoBehaviour
{
    [SerializeField] private EN_BuildType m_BuildType;

    [SerializeField] private int m_TimeToEXPMultiplier;

    private float m_TimeSpentInBuild;

    private bool m_IsFrogWorking;

    private Frog m_CurrentFrogData;
    FrogFarm m_currentFarm;

    private void Update()
    {
        if (m_IsFrogWorking)
        {
            m_TimeSpentInBuild += Time.deltaTime;
        }
    }

    public void SetFrogData() //OnDrop > Get Frog Component > Disable Graphics
    {

    }


    public void StartWorking(Frog frog)
    {
        m_IsFrogWorking = true;
    }

    public void CancelWorking()
    {
        m_IsFrogWorking = false;
        AddExpToFarm();
        m_TimeSpentInBuild = 0;
    }

    public void AddExpToFarm()
    {
        switch (m_BuildType)
        {
            case EN_BuildType.RUN_BUILD:
                m_currentFarm.AddXp((int)(m_CurrentFrogData.m_frogDynamicData.m_RunLevel * m_TimeSpentInBuild * m_TimeToEXPMultiplier));
                break;
            case EN_BuildType.FLY_BUILD:
                m_currentFarm.AddXp((int)(m_CurrentFrogData.m_frogDynamicData.m_FlyLevel * m_TimeSpentInBuild * m_TimeToEXPMultiplier));
                break;
            case EN_BuildType.SWIM_BUILD:
                m_currentFarm.AddXp((int)(m_CurrentFrogData.m_frogDynamicData.m_SwimLevel * m_TimeSpentInBuild * m_TimeToEXPMultiplier));
                break;
            default:
                Log.Error("Cannot find the correct type");
                break;
        }
    }
}
