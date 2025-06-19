[System.Serializable]
public class FrogDynamicData : DynamicScriptableObject
{
    public EN_FrogRarity m_rarity;
    public string m_frogName;

    public FrogDynamicData(EN_FrogRarity rarity, string frogName)
    {
        m_rarity = rarity;
        m_frogName = frogName;
    }
}
