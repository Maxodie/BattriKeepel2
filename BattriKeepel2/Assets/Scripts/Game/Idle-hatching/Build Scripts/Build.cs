using UnityEngine; 

public class Build : MonoBehaviour
{
    [SerializeField] private EN_BuildType m_BuildType;

    [SerializeField] private int m_TimeToEXPMultiplier;

    private Frog m_CurrentFrogData;
    private float m_TimeSpentInBuild;

    private bool m_IsFrogWorking;

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

    public void StartWorking()
    {
        m_IsFrogWorking = true;
    }

    public void CancelWorking()
    {
        m_IsFrogWorking = false;
        AddExpToFrog();
        m_TimeSpentInBuild = 0;
    }

    public void AddExpToFrog()
    {
        switch (m_BuildType)
        {
            case EN_BuildType.RUN_BUILD:
                m_CurrentFrogData.AddExpAmount(EN_FrogLevels.RUN, (int)m_TimeSpentInBuild * m_TimeToEXPMultiplier);
                break;
            case EN_BuildType.FLY_BUILD:
                m_CurrentFrogData.AddExpAmount(EN_FrogLevels.FLY, (int)m_TimeSpentInBuild * m_TimeToEXPMultiplier);
                break;
            case EN_BuildType.SWIM_BUILD:
                m_CurrentFrogData.AddExpAmount(EN_FrogLevels.SWIM, (int)m_TimeSpentInBuild * m_TimeToEXPMultiplier);
                break;
            default:
                Log.Error("Cannot find the correct type");
                break;
        }
    }
}
