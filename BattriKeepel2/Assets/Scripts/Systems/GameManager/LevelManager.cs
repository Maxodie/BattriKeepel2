using GameEntity;
using UnityEngine;

public class LevelManager : GameManager {
    [Header("Player")]
    [SerializeField] Transform m_playerTransform;
    [SerializeField] Transform m_cameraTr;

    Player m_player;
    SO_GameLevelData m_currentLevelData;
    LevelPhaseContext m_phaseContext;

#if UNITY_EDITOR || DEVELOPMENT_BUILD
    [Header("Debug Data")]
    [SerializeField] SO_PlayerData m_playerDebugData;
    [SerializeField] SO_GameLevelData m_debugLevelData;
#endif

    protected override void Awake()
    {
        // Player data setup
        if(GameInstance.GetCurrentPlayerData())
        {
            m_player = new Player(GameInstance.GetCurrentPlayerData(), m_playerTransform);
        }
        else
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            m_player = new Player(m_playerDebugData, m_playerTransform);
            Log.Warn<GameManagerLogger>("Player Data could not be found, debug data selected by default");
#else
            Log.Error<GameManagerLogger>("Player Data could not be found");
#endif
        }

        // Level data setup
        if(!GameInstance.GetCurrentLevelData())
#if UNITY_EDITOR || DEVELOPMENT_BUILD
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
        m_phaseContext.StartContext(m_currentLevelData);
        m_phaseContext.BindOnPhaseEndEvent(OnGamePhaseContextEnd);
    }

    protected override void Update()
    {
        m_phaseContext.Update();
        m_player.Update();
    }

    protected override void FixedUpdate() {

    }

    protected override void OnUIManagerCreated() {

    }

    void OnGamePhaseContextEnd()
    {
        Log.Info<GameManagerLogger>("Level Manager detect the end of the game phases context");
    }
}
