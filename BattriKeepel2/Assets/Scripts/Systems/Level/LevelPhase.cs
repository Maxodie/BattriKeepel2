using System;
using UnityEngine.Events;

public class LevelPhaseLogger : Logger
{

}

public abstract class LevelPhase
{
    SO_LevelPhase m_levelPhase;
    protected LevelPhaseContext m_context;

    public void Init(SO_LevelPhase levelPhase, LevelPhaseContext context)
    {
        m_context = context;
        m_levelPhase = levelPhase;
    }

    public abstract void OnStart();
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

    UnityEvent m_onPhaseContextEnd = new();

    public void StartContext(SO_GameLevelData levelData)
    {
        m_currentPhases = new LevelPhase[levelData.levelPhases.Length];
        m_currentPhaseID = 0;

        Type phaseType;
        for(int i = 0; i < m_currentPhases.Length; i++)
        {
            phaseType = Type.GetType(levelData.levelPhases[i].phaseTypeName);
            m_currentPhases[i] = (LevelPhase)Activator.CreateInstance(phaseType);
;
            m_currentPhases[i].Init(levelData.levelPhases[i], this);
        }

        SetupCurrentPhase();
    }

    public void BindOnPhaseEndEvent(UnityAction fun)
    {
        m_onPhaseContextEnd.AddListener(fun);
    }

    public void Update()
    {
        m_currentPhase?.Update();
    }

    public void EndPhase()
    {
        if(m_currentPhaseID + 1 >= m_currentPhases.Length)
        {
            Log.Info<LevelPhaseLogger>("phase context has ended");
            m_onPhaseContextEnd.Invoke();
            return;
        }

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
        m_currentPhase.OnStart();
    }
}
