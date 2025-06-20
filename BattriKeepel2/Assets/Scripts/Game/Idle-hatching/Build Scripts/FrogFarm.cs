using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class FrogFarmDynamicSO : DynamicScriptableObject
{
    public int m_currentXP = 0;
    public int m_currentXPThreashold;
    public int m_level = 1;
}

public class FrogFarm : IGameEntity
{
    FrogFarmDynamicSO frogFarmData;
    float m_xpMultiplier = 1.5f;
    int m_maxLevel = 100;

    FrogFarmUIMenu m_uiMenu;

    public FrogFarm(FrogFarmUIMenu uiMenu, int startXpThreashold)
    {
        m_uiMenu = uiMenu;

        List<FrogFarmDynamicSO> frogFarmDatas = SaveSystem.Load<FrogFarmDynamicSO>(this);
        frogFarmData = frogFarmDatas != null ? frogFarmDatas[0] : new();

        frogFarmData.m_currentXPThreashold = startXpThreashold;
        uiMenu.SetXpFill(frogFarmData.m_currentXP, frogFarmData.m_currentXPThreashold, frogFarmData.m_level);
    }

    public void AddXp(int amount)
    {
        frogFarmData.m_currentXP += amount;
        CheckforLevel();
        m_uiMenu.SetXpFill(frogFarmData.m_currentXP, frogFarmData.m_currentXPThreashold, frogFarmData.m_level);

        SaveSystem.Save<FrogFarmDynamicSO>(frogFarmData, this, 0);
    }

    void CheckforLevel()
    {
        if(m_maxLevel <= frogFarmData.m_level && frogFarmData.m_currentXP >= frogFarmData.m_currentXPThreashold)
        {
            frogFarmData.m_level ++;
            frogFarmData.m_currentXP = 0;
            frogFarmData.m_currentXPThreashold = Mathf.RoundToInt((float)frogFarmData.m_currentXPThreashold * m_xpMultiplier);
        }
        else if(m_maxLevel <= frogFarmData.m_level)
        {
            frogFarmData.m_level = m_maxLevel;
        }
    }
}
