using UnityEngine;

[System.Serializable]
public class Frog
{
    public string m_Name;
    public EN_FrogColors m_Color;
    public EN_FrogRarity m_Rarity;
    public int m_RunLevel;
    public int m_FlyLevel;
    public int m_SwimLevel;

    public Frog(string name, EN_FrogColors color, EN_FrogRarity rarity, SO_FrogLevelData frogLevelData)
    {
        m_Name = name;
        m_Color = color;
        m_Rarity = rarity;

        m_RunLevel = Random.Range(frogLevelData.m_minimumRunLevel, frogLevelData.m_maximumRunLevel);
        m_FlyLevel = Random.Range(frogLevelData.m_minimumFlyLevel, frogLevelData.m_maximumFlyLevel);
        m_SwimLevel = Random.Range(frogLevelData.m_minimumSwimLevel, frogLevelData.m_maximumSwimLevel);
    }
}
