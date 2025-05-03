using GameEntity;
using UnityEngine;

public class LevelManager : GameManager {
    [Header("Player")]
    [SerializeField] SO_PlayerData m_playerData;
    [SerializeField] Transform m_playerTransform;
    Player m_player;

    [SerializeField] SO_GameLevelData m_gameLevelData;

    [Header("Collisions")]
    [SerializeField] SO_CollisionManagerData m_collisionManagerData;
    CollisionManager m_collisionManager;

#if UNITY_EDITOR
    [Header("Debug Data")]
    [SerializeField] SO_PlayerData playerDebugData;
#endif

    protected override void Awake()
    {
        m_collisionManager = CollisionManager.GetInstance();
        m_collisionManager.SetParameters(m_collisionManagerData);

        m_player = new Player(m_playerData, m_playerTransform);

        if(GameInstance.GetCurrentPlayerData())
        {
            m_player = new Player(GameInstance.GetCurrentPlayerData(), m_playerTransform);
        }
        else
        {
        }
    }

    protected override void Update()
    {
        m_player.Update();
        m_collisionManager.Update();
    }

    protected override void OnUIManagerCreated() {

    }
}
