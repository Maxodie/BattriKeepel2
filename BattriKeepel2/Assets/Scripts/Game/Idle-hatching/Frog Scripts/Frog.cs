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

        m_Levelling = new FrogLevelling();
        m_Interaction = new FrogInteraction(m_Graphics.rb, m_Behavior);

        m_Behavior.InitFrogBehavior(m_Interaction, m_Graphics, quality.leapCooldown);

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

    public void SetActive(bool state)
    {
        m_Behavior.m_isRunning = state;
        m_Interaction.canInputInteraction = state;
    }

    public bool IsInInteraction()
    {
        return m_Interaction.isInInputInteraction;
    }

    public void SetPosition(Vector2 pos)
    {
        m_Graphics.transform.position = pos;
    }

    public void SetLevelUp(EN_FrogLevels type)
    {
        m_Levelling.SetLevelUp(type);
    }

    public void OnLevelUpdate()
    {
        m_Graphics.OnChangeLevel();
    }
}
