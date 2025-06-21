using System.Collections.Generic;
using UnityEngine;

public class BossAttackPhase
{
    SO_BossAttackData[] m_attacks;
    int m_currentAttackID;

    List<BossAttack> m_currentAttack;
    int m_currentAttackCount;

    AttackGraphicsPool m_attackPool;
    GameEntity.Player m_player;
    BossEntity m_boss;

    public BossAttackPhase(BossEntity boss, SO_BossAttackData[] attackDatas, AttackGraphicsPool attackPool, GameEntity.Player player)
    {
        m_boss = boss;
        m_attackPool = attackPool;
        m_player = player;

        m_attacks = attackDatas;
        m_currentAttackID = 0;
        m_currentAttackCount = 0;
        m_currentAttack = new();

        LoopAttack();
    }

    public void Update()
    {
        for(int i = 0; i < m_currentAttack.Count; i++)
        {
            if(!m_currentAttack[i].m_attackGraphics.isActive)
            {
                m_currentAttack.RemoveAt(i);
                i--;
                continue;
            }

            m_currentAttack[i].Update();
        }
    }

    public void StartCurrentAttack(int id)
    {
        m_currentAttack.Add(new(m_boss, m_attackPool, m_attacks[id], m_player));
    }

    private async Awaitable HandleAttacks()
    {
        for (int i = 0; i < m_attacks.Length; i++)
        {
            StartCurrentAttack(i);
            await Awaitable.WaitForSecondsAsync(m_attacks[i].intervalBeforeNextAttack);
        }

        LoopAttack();
    }

    public void LoopAttack()
    {
        _ = HandleAttacks();
    }

    public void Clear()
    {
        for(int i = 0; i < m_currentAttack.Count; i++)
        {
            m_currentAttack[i].Clean();
        }

        m_currentAttack.Clear();
    }
}

public class BossAttackPhaseSystem
{
    List<BossAttackPhase> currentPhases = new();
    public int m_currentPhaseID = 0;
    AttackGraphicsPool m_attackPool;
    GameEntity.Player m_player;
    SO_BossAttackPhase[] m_attackDatas;
    BossEntity m_boss;

    public BossAttackPhaseSystem(BossEntity boss, SO_BossAttackPhase[] attackDatas, AttackGraphicsPool attackPool, GameEntity.Player player)
    {
        m_attackDatas = attackDatas;
        m_attackPool = attackPool;
        m_player = player;
        m_boss = boss;
    }

    public void UpdatePhase()
    {
        for(int i = 0; i < currentPhases.Count; i++)
        {
            currentPhases[i].Update();
        }
    }

    public void StartPhaseSystem()
    {
        currentPhases.Add(new BossAttackPhase(m_boss, m_attackDatas[m_currentPhaseID].bossAttackData, m_attackPool, m_player));
    }

    public void SwitchToNextPhase()
    {
        Clear();

        if(m_currentPhaseID < m_attackDatas.Length - 1)
        {
            m_currentPhaseID++;
        }

        StartPhaseSystem();
    }

    public void Clear()
    {
        foreach(BossAttackPhase phase in currentPhases)
        {
            phase.Clear();
        }
    }
}
