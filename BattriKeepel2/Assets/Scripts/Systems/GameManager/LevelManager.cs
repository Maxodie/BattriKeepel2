using GameEntity;
using UnityEngine;
using System.Collections;
using Unity.Mathematics;

public class LevelManager : GameManager {
    [Header("Player")]
    [SerializeField] Transform m_playerTransform;
    [SerializeField] Transform m_cameraTr;
    [SerializeField] float m_endGameTimer = 1.5f;

    [Header("UI")]
    [SerializeField] Transform m_canvasSpawnLocation;
    [SerializeField] SO_EndGameUIData m_endgameData;

    public Player m_player;
    SO_PlayerData m_playerData;
    SO_GameLevelData m_currentLevelData;

    LevelPhaseContext m_phaseContext;
    UIGameTransition m_transition;
    CameraEffect camEffect;

    public AttackGraphicsPool m_bulletPool;
    [Header("Bullet pool")]
    [SerializeField] Transform m_bulletPoolParent;

    [Header("Level")]
    [SerializeField] private WallGraphics wallBound;

#if UNITY_EDITOR || DEVELOPMENT_BUILD
    [Header("Debug Data")]
    [SerializeField] SO_PlayerData m_playerDebugData;
    [SerializeField] SO_GameLevelData m_debugLevelData;
#endif

    protected override void Awake()
    {
        base.Awake();
        // Player data setup
        if(GameInstance.GetCurrentPlayerData())
        {
            m_playerData = GameInstance.GetCurrentPlayerData();
        }
        else
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            m_playerData = m_playerDebugData;
            Log.Warn<GameManagerLogger>("Player Data could not be found, debug data selected by default");
#else
            Log.Error<GameManagerLogger>("Player Data could not be found");
#endif
        }

        m_bulletPool = new(m_bulletPoolParent);

        m_player = new Player(m_bulletPool, m_playerData, m_playerTransform);
        camEffect = new(Camera.main.transform, 20, 0.3f);

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
        m_phaseContext.StartContext(m_currentLevelData, this);
        m_phaseContext.BindOnPhaseEndEvent(OnGamePhaseContextEnd);
        m_phaseContext.BindOnPhaseTransitionEvent(OnGamePhaseEnd);

        UIDataResult result = UIManager.GenerateUIData(m_currentLevelData.transitionPrefab, m_canvasSpawnLocation);
        m_transition = (UIGameTransition)result.Menu;
        m_transition.ActiveTransition(false);
        m_transition.BindOnTransitionEnd(OnGamePhaseTransitionEnd);

        m_playerTransform.position = GraphicsManager.Get().GetCameraLocation((int)SpawnDir.South) + new Vector2(0, 5);

        CreateBounds();
    }

    protected override void Update()
    {
        m_phaseContext.Update();

        if(m_phaseContext.IsContextPhaseActive())
        {
            m_player.Update();
            if(m_player.IsDead())
            {
                m_phaseContext.StopContext(false);
            }
        }
    }

    protected override void OnUIManagerCreated()
    {

    }

    void OnGamePhaseEnd()
    {
        m_player.SetActive(false);
        m_player.ClearBullets();

        if(m_transition)
        {
            m_transition.SetTransition(m_phaseContext.GetCurrentLevelPhase().m_levelPhase, m_playerData);
            m_transition.ActiveTransition(true);
        }
    }

    private void CreateBounds()
    {
        SpawnDir[] directions = {SpawnDir.North, SpawnDir.South, SpawnDir.East, SpawnDir.West};

        for (int i = 0; i < 4; i++)
        {
            WallGraphics wall = GraphicsManager.Get().GenerateVisualInfos<WallGraphics>(wallBound, directions[i], Vector3.zero, quaternion.identity, this);
            if (i >= 2)
            {
                wall.transform.rotation = Quaternion.Euler(0, 0, 90);
            }
        }
    }

    void OnGamePhaseTransitionEnd()
    {
        m_player.SetActive(true);
        m_transition.ActiveTransition(false);
    }

    void OnGamePhaseContextEnd(bool isWin)
    {
        Log.Info<GameManagerLogger>("Level Manager detect the end of the game phases context");

        StartCoroutine(WaitforEndUI(isWin));
    }

    IEnumerator WaitforEndUI(bool isWin)
    {
        camEffect.StartShake(m_endGameTimer);
        yield return new WaitForSeconds(m_endGameTimer);
        EndGame(isWin);
    }

    void EndGame(bool isWin)
    {
        FrogDynamicData frogData = FrogGenerator.Get().GenerateFrog();
        UIDataResult result = UIManager.GenerateUIData(m_endgameData, m_canvasSpawnLocation);
        ((EndGameUI)result.Menu).SetWinState(isWin);
        ((EndGameUI)result.Menu).SetNewFrogDataInfos(frogData);

        m_player.SetActive(false);
    }
}
