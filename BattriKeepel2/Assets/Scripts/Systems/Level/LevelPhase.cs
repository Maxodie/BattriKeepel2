using System;
using UnityEngine.Events;
using UnityEngine;

public class LevelPhaseLogger : Logger
{

}

public abstract class LevelPhase
{
    public SO_LevelPhase m_levelPhase { get; private set; }
    protected LevelPhaseContext m_context;

    public void Init(SO_LevelPhase levelPhase, LevelPhaseContext context)
    {
        m_context = context;
        m_levelPhase = levelPhase;
    }

    public abstract void OnStart(LevelManager levelManager);
    public abstract void OnEnd();
    public abstract void Update();

    protected void EndPhase()
    {
        m_context.EndPhase();
    }
}

public class LevelPhaseContext
{
    LevelPhase[] m_currentPhases;
    LevelPhase m_currentPhase;
    int m_currentPhaseID = 0;

    UnityEvent<bool> m_onPhaseContextEnd = new();
    UnityEvent m_onPhaseTransition = new();

    float m_phaseTransitionDelay = 0.0f;
    Awaitable m_waitForTransition = null;
    GameEntityMonoBehaviour m_monoContext;
    bool m_isContextPhaseActive = false;
    LevelManager m_levelManager;

    public void StartContext(SO_GameLevelData levelData, LevelManager levelManager)
    {
        m_levelManager = levelManager;
        m_currentPhases = new LevelPhase[levelData.levelPhases.Length];
        m_currentPhaseID = 0;
        m_phaseTransitionDelay = levelData.phaseTransitionDelay;
        m_monoContext = levelManager;

        Type phaseType;
        for(int i = 0; i < m_currentPhases.Length; i++)
        {
            phaseType = Type.GetType(levelData.levelPhases[i].phaseTypeName);
            m_currentPhases[i] = (LevelPhase)Activator.CreateInstance(phaseType);
            m_currentPhases[i].Init(levelData.levelPhases[i], this);
        }

        SetupCurrentPhase();
    }

    public void StopContext(bool isWin)
    {
        m_isContextPhaseActive = false;
        m_onPhaseContextEnd.Invoke(isWin);
    }

    public LevelPhase GetCurrentLevelPhase()
    {
        return m_currentPhase;
    }

    public bool IsContextPhaseActive()
    {
        return m_isContextPhaseActive;
    }

    public void BindOnPhaseEndEvent(UnityAction<bool> fun)
    {
        m_onPhaseContextEnd.AddListener(fun);
    }

    public void BindOnPhaseTransitionEvent(UnityAction fun)
    {
        m_onPhaseTransition.AddListener(fun);
    }

    public void Update()
    {
        if(m_isContextPhaseActive)
        {
            m_currentPhase?.Update();
        }
    }

    public void EndPhase()
    {
        m_isContextPhaseActive = false;

        if(m_currentPhaseID + 1 >= m_currentPhases.Length)
        {
            m_onPhaseContextEnd.Invoke(true);
            return;
        }

        if(m_waitForTransition != null && !m_waitForTransition.IsCompleted)
        {
            m_waitForTransition.Cancel();
        }

        m_waitForTransition = NextPhaseTransition();
        m_onPhaseTransition.Invoke();
    }

    async Awaitable NextPhaseTransition()
    {
        await Awaitable.WaitForSecondsAsync(m_phaseTransitionDelay);
        SwitchToNextPhase();
    }

    void SwitchToNextPhase()
    {
        m_currentPhase?.OnEnd();
        ClearCurrentPhase();

        m_currentPhaseID++;
        SetupCurrentPhase();
    }

    void ClearCurrentPhase()
    {
        m_currentPhase = null;
        m_currentPhases[m_currentPhaseID] = null;
    }

    void SetupCurrentPhase()
    {
        m_currentPhase = m_currentPhases[m_currentPhaseID];
        m_currentPhase.OnStart(m_levelManager);
        m_isContextPhaseActive = true;
    }
}
