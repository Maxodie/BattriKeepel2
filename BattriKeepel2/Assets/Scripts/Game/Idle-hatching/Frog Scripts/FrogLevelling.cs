[System.Serializable]
public class FrogLevelling
{
    private Frog m_frogData;

    public void InitFrogLevelling(Frog f, SO_FrogLevelData frogLevelData)
    {
        m_frogData = f;

        f.OnLevelUpdate();
    }

    public void SetLevelUp(EN_FrogLevels type)
    {
        switch (type)
        {
            case EN_FrogLevels.RUN:
                m_frogData.m_frogDynamicData.m_RunLevel ++;
                break;
            case EN_FrogLevels.FLY:
                m_frogData.m_frogDynamicData.m_FlyLevel ++;
                break;
            case EN_FrogLevels.SWIM:
                m_frogData.m_frogDynamicData.m_SwimLevel ++;
                break;
            default:
                Log.Error("Cannot find the correct type");
                break;
        }

        m_frogData.OnLevelUpdate();
    }
}
