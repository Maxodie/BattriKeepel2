using UnityEngine;

public class Frog : MonoBehaviour
{
    public string m_frogName;
    public EN_FrogColors m_Color;
    public EN_FrogRarity m_Rarity;

    [SerializeField] private FrogGraphics m_Graphics;
    [SerializeField] private FrogBehavior m_Behavior;
    [SerializeField] private FrogLevelling m_Levelling;

    public void Init(string name, EN_FrogColors color, EN_FrogRarity rarity, SO_FrogLevelData frogLevelData)
    {
        m_frogName = name;
        m_Color = color;
        m_Rarity = rarity;

        m_Graphics.InitFrogGraphics(this);
        m_Behavior.InitFrogBehavior(this);
        m_Levelling.InitFrogLevelling(this, frogLevelData);
    }

    public void Update()
    {
        m_Behavior.UpdateBehavior();
    }

    public void AddExpAmount(EN_FrogLevels type, int amount)
    {
        m_Levelling.AddExpAmount(type, amount);
    }

    public void SetLevelUp(EN_FrogLevels type)
    {
        m_Levelling.SetLevelUp(type);
    }
}
