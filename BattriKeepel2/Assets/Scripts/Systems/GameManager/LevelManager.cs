using GameEntity;
using UnityEngine;

public class LevelManager : GameManager {
    [Header("Player")]
    [SerializeField] Transform m_playerTransform;
    [SerializeField] Transform m_cameraTr;

    [Header("Collisions")]
    [SerializeField] SO_CollisionManagerData m_collisionManagerData;
    CollisionManager m_collisionManager;

    [Header("UI")]
    [SerializeField] Transform m_canvasSpawnLocation;

    Player m_player;
    SO_PlayerData m_playerData;
    SO_GameLevelData m_currentLevelData;
    LevelPhaseContext m_phaseContext;

#if UNITY_EDITOR
    [Header("Debug Data")]
    [SerializeField] SO_PlayerData m_playerDebugData;
    [SerializeField] SO_GameLevelData m_debugLevelData;
#endif

    protected override void Awake()
    {
        m_collisionManager = CollisionManager.GetInstance();
        m_collisionManager.SetParameters(m_collisionManagerData);

        // Player data setup
        if(GameInstance.GetCurrentPlayerData())
        {
            m_playerData = GameInstance.GetCurrentPlayerData();
        }
        else
        {
#if UNITY_EDITOR
            m_playerData = m_playerDebugData;
            Log.Warn<GameManagerLogger>("Player Data could not be found, debug data selected by default");
#else
            Log.Error<GameManagerLogger>("Player Data could not be found");
#endif
        }

        m_player = new Player(m_playerData, m_playerTransform);

        // Level data setup
        if(!GameInstance.GetCurrentLevelData())
#if UNITY_EDITOR
        {
            GameInstance.SetCurrentBossLevel(m_debugLevelData);
            Log.Warn<GameManagerLogger>("Game Level Data could not be found, debug data selected by default");
        }
#else
        {
            Log.Error<GameManagerLogger>("Game Level Data could not be found");
        }
#endif

        m_currentLevelData = GameInstance.GetCurrentLevelData();

        if(m_currentLevelData.levelArtData)
        {
            GraphicsManager.Get().GenerateVisualInfos<LevelArtGraphicsEntity>(m_currentLevelData.levelArtData.gameArt, transform, null);
        }
        else
        {
            Log.Error<GameManagerLogger>("Game Visual could not be found");
        }

        // Setup phase context
        m_phaseContext = new LevelPhaseContext();
        m_phaseContext.StartContext(m_currentLevelData, this);
        m_phaseContext.BindOnPhaseEndEvent(OnGamePhaseContextEnd);
        m_phaseContext.BindOnPhaseTransitionEvent(OnGamePhaseEnd);
    }

    protected override void Update()
    {
        m_phaseContext.Update();

        if(m_phaseContext.IsContextPhaseActive())
        {
            m_player.Update();
        }

        m_collisionManager.Update();
    }

    protected override void OnUIManagerCreated() {

    }

    void OnGamePhaseEnd()
    {
        UIDataResult result = UIManager.GenerateUIData(m_currentLevelData.transitionPrefab, m_canvasSpawnLocation);
        UIGameTransition transition = (UIGameTransition)result.Menu;
        if(transition)
        {
            transition.SetTransition(m_phaseContext.GetCurrentLevelPhase().m_levelPhase, m_playerData);
        }
    }

    void OnGamePhaseContextEnd()
    {
        Log.Info<GameManagerLogger>("Level Manager detect the end of the game phases context");
    }
}
