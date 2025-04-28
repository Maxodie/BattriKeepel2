using GameEntity;
using UnityEngine;

public class LevelManager : GameManager {
    [SerializeField] Player player;
    [SerializeField] SO_PlayerData playerData;
    [SerializeField] Transform playerTransform;
    private CollisionManager m_collisionManager;
    [SerializeField] SO_CollisionManagerData m_collisionManagerData;

    protected override void Awake()
    {
        player = new Player(playerData, playerTransform);
        m_collisionManager = CollisionManager.GetInstance();
        m_collisionManager.SetParameters(m_collisionManagerData);
    }

    protected override void Update()
    {
        player.Update();
        m_collisionManager.Update();
    }

    protected override void OnUIManagerCreated() {

    }
}
