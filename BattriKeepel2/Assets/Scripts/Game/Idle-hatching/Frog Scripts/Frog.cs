using Inputs;
using UnityEngine;

public class Frog : IGameEntity
{
    public string m_frogName;
    public EN_FrogColors m_Color;
    public EN_FrogRarity m_Rarity;

    private FrogGraphics m_Graphics;
    private FrogBehavior m_Behavior;
    private FrogLevelling m_Levelling;
    private FrogInteraction m_Interaction;
    private InputManager m_InputManager;

    public Frog(string name, EN_FrogColors color, EN_FrogRarity rarity, InputManager inputManager, SO_FrogLevelData frogLevelData)
    {
        m_frogName = name;
        m_Color = color;
        m_Rarity = rarity;
        m_InputManager = inputManager;

        m_Behavior = new FrogBehavior();

        m_Graphics = GraphicsManager.Get().GenerateVisualInfos<FrogGraphics>(frogLevelData.graphics, new Vector2(), Quaternion.identity, this, false);
        m_Graphics.InitFrogGraphics(this);
        m_Behavior.InitFrogBehavior(m_Graphics, frogLevelData.leapCooldown);
        m_Levelling = new FrogLevelling();
        m_Interaction = new FrogInteraction(m_Graphics.rb, m_Behavior);
        m_Levelling.InitFrogLevelling(this, frogLevelData);

        BindActions();
    }

    private void BindActions()
    {
        m_InputManager.BindPosition(m_Interaction.OnPosition);
        m_InputManager.BindPress(m_Interaction.OnPress);
    }

    public void Update()
    {
        m_Behavior.UpdateBehavior();
    }

    public bool HandleMovement()
    {
        return m_Interaction.HandleMovement();
    }

    public void AddExpAmount(EN_FrogLevels type, int amount)
    {
        m_Levelling.AddExpAmount(type, amount);
        m_Levelling.CheckForLevelUp(type);
    }

    public void SetLevelUp(EN_FrogLevels type)
    {
        m_Levelling.SetLevelUp(type);
    }
}
