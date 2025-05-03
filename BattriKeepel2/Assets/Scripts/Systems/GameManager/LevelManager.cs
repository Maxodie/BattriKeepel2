using GameEntity;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : GameManager {
    [Header("Player")]
    [SerializeField] SO_PlayerData m_playerData;
    [SerializeField] Transform m_playerTransform;
    Player m_player;

    [Header("Boss")]
    [SerializeField] Transform m_bossSpawnPoint;
    BossEntity m_boss;

    [Header("Collisions")]
    [SerializeField] SO_CollisionManagerData m_collisionManagerData;
    CollisionManager m_collisionManager;

#if UNITY_EDITOR
    [Header("Debug Data")]
    [SerializeField] SO_BossScriptableObject bossDebugData;
    [SerializeField] SO_PlayerData playerDebugData;
#endif

    protected override void Awake()
    {
        m_collisionManager = CollisionManager.GetInstance();
        m_collisionManager.SetParameters(m_collisionManagerData);

        m_player = new Player(m_playerData, m_playerTransform);

        if(GameInstance.GetCurrentBossLevel())
        {
            m_boss = new BossEntity(GameInstance.GetCurrentBossLevel().bossData, m_bossSpawnPoint);
        }
        else
        {
#if UNITY_EDITOR
            m_boss = new BossEntity(bossDebugData, m_bossSpawnPoint);
#elif DEVELOPMENT_BUILD
            /*Log.Error<GameManagerLogger>("no SO_BossScriptableObject in GameInstance. debug data has been taken");*/
#endif
        }

        if(GameInstance.GetCurrentPlayerData())
        {
            m_player = new Player(GameInstance.GetCurrentPlayerData(), m_playerTransform);
        }
        else
        {
#if UNITY_EDITOR
            m_player = new Player(playerDebugData, m_playerTransform);
#elif DEVELOPMENT_BUILD
            /*Log.Error<GameManagerLogger>("no SO_BossScriptableObject in GameInstance. debug data has been taken");*/
#endif
        }
    }

    protected override void Update()
    {
        m_player.Update();
        m_boss.Update();
        m_collisionManager.Update();
    }

    protected override void OnUIManagerCreated() {

    }
}
