using UnityEngine;

[System.Serializable]
public class FrogLevelling
{
    private Frog m_frogData;

    public int m_RunLevel;
    public int m_RunLevelEXP = 0;
    public int m_FlyLevel;
    public int m_FlyLevelEXP = 0;
    public int m_SwimLevel;
    public int m_SwimLevelEXP = 0;

    private int m_MaxLevel = 10;

    [SerializeField] private SO_MaxExpPoints SO_MaxExpPoints;

    public void InitFrogLevelling(Frog f, SO_FrogLevelData frogLevelData)
    {
        m_frogData = f;

        m_RunLevel = Random.Range(frogLevelData.m_minimumRunLevel, frogLevelData.m_maximumRunLevel);
        m_FlyLevel = Random.Range(frogLevelData.m_minimumFlyLevel, frogLevelData.m_maximumFlyLevel);
        m_SwimLevel = Random.Range(frogLevelData.m_minimumSwimLevel, frogLevelData.m_maximumSwimLevel);
    }

    public void AddExpAmount(EN_FrogLevels type, int amount)
    {
        switch (type)
        {
            case EN_FrogLevels.RUN:
                m_RunLevelEXP += amount;
                break;
            case EN_FrogLevels.FLY:
                m_FlyLevelEXP += amount;
                break;
            case EN_FrogLevels.SWIM:
                m_SwimLevelEXP += amount;
                break;
            default:
                Log.Error("Cannot find the correct type");
                break;
        }
    }

    public void SetLevelUp(EN_FrogLevels type)
    {
        switch (type)
        {
            case EN_FrogLevels.RUN:
                m_RunLevel += 1;
                m_RunLevelEXP = 0;
                break;
            case EN_FrogLevels.FLY:
                m_FlyLevel += 1;
                m_FlyLevelEXP = 0;
                break;
            case EN_FrogLevels.SWIM:
                m_SwimLevel += 1;
                m_SwimLevelEXP = 0;
                break;
            default:
                Log.Error("Cannot find the correct type");
                break;
        }
    }

    public void CheckForLevelUp(EN_FrogLevels type)
    {
        int currentLevel = 0;
        int currentEXP = 0;
        switch (type)
        {
            case EN_FrogLevels.RUN:
                currentLevel = m_RunLevel;
                currentEXP = m_RunLevelEXP;
                break;
            case EN_FrogLevels.FLY:
                currentLevel = m_FlyLevel;
                currentEXP = m_FlyLevelEXP;
                break;
            case EN_FrogLevels.SWIM:
                currentLevel = m_SwimLevel;
                currentEXP = m_SwimLevelEXP;
                break;
            default:
                Log.Error("Cannot find the correct type");
                break;
        }

        if(currentLevel < m_MaxLevel) //éviter que la frog passe au dessus du niveau 10
        {
            if(currentEXP >= SO_MaxExpPoints.EXPPointsToGainLevel[currentLevel])
            {
                SetLevelUp(type);
                AddExpAmount(type, currentEXP - SO_MaxExpPoints.EXPPointsToGainLevel[currentLevel]); //On ajoute au niveau suivant l'EXP restant si y'en a trop, c'est po perdu quoient
            }
        }
    }
}
