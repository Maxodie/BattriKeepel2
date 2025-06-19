using Inputs;
using UnityEngine;

public class Frog : IGameEntity
{
    public string m_frogName;
    public FrogDynamicData m_frogDynamicData;
    public EN_FrogRarity m_Rarity;

    private FrogGraphics m_Graphics;
    private FrogBehavior m_Behavior;
    private FrogLevelling m_Levelling;
    private FrogInteraction m_Interaction;
    private InputManager m_InputManager;

    public Frog(FrogDynamicData frogDynamicData, InputManager inputManager)
    {
        m_frogDynamicData = frogDynamicData;
        m_InputManager = inputManager;

        m_Behavior = new FrogBehavior();

        SO_FrogLevelData quality = FrogGenerator.Get().QueryRarityLevel(frogDynamicData.m_rarity);
        m_Graphics = GraphicsManager.Get().GenerateVisualInfos<FrogGraphics>(quality.graphics, new Vector2(), Quaternion.identity, this, false);
        m_Graphics.InitFrogGraphics(m_frogDynamicData);
        m_Behavior.InitFrogBehavior(m_Graphics, quality.leapCooldown);
        m_Levelling = new FrogLevelling();
        m_Interaction = new FrogInteraction(m_Graphics.rb, m_Behavior);
        m_Levelling.InitFrogLevelling(this, quality);

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
