using Unity.Android.Gradle.Manifest;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_EggRarityRate", menuName = "Scriptable Objects/SO_EggRarityRate")]
public class SO_EggRarityRate : ScriptableObject
{
    public int m_CommonRate;
    public int m_UncommonRate;
    public int m_RareRate;
    public int m_EpicRate;
    public int m_KeepelRate;

    public int TotalRate()
    {
        return m_CommonRate + m_UncommonRate + m_RareRate + m_EpicRate + m_KeepelRate;
    }

    public EN_FrogRarity CompareNumberToRate(int number)
    {
        if (number <= m_CommonRate) { return EN_FrogRarity.COMMON; }
        else if (number <= m_UncommonRate + m_CommonRate) { return EN_FrogRarity.UNCOMMUN; }
        else if (number <= m_RareRate + m_UncommonRate + m_CommonRate) { return EN_FrogRarity.RARE; }
        else if (number <= m_EpicRate + m_RareRate + m_UncommonRate + m_CommonRate) { return EN_FrogRarity.EPIC; }
        else { return EN_FrogRarity.KEEPEL; }
    }
}
