using GameEntity;
using UnityEngine;

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

    protected override void Awake()
    {
        m_collisionManager = CollisionManager.GetInstance();
        m_collisionManager.SetParameters(m_collisionManagerData);

        m_player = new Player(m_playerData, m_playerTransform);
        //m_boss = new BossEntity(GameInstance.GetCurrentBossLevel().bossData, m_bossSpawnPoint);
    }

    protected override void Update()
    {
        m_player.Update();
        //m_boss.Update();
        m_collisionManager.Update();
    }

    protected override void OnUIManagerCreated() {

    }
}
