using UnityEngine;

[System.Serializable]
public class FrogDynamicData : DynamicScriptableObject
{
    public EN_FrogRarity m_rarity;
    public string m_frogName;

    public int m_RunLevel;
    public int m_FlyLevel;
    public int m_SwimLevel;

    public FrogDynamicData(EN_FrogRarity rarity, string frogName)
    {
        m_rarity = rarity;
        m_frogName = frogName;

        SO_FrogLevelData quality = FrogGenerator.Get().QueryRarityLevel(m_rarity);
        m_RunLevel = Random.Range(quality.m_minimumRunLevel, quality.m_maximumRunLevel);
        m_FlyLevel = Random.Range(quality.m_minimumFlyLevel, quality.m_maximumFlyLevel);
        m_SwimLevel = Random.Range(quality.m_minimumSwimLevel, quality.m_maximumSwimLevel);
    }
}
