using GameEntity.Player;
using UnityEngine;

public class LevelManager : GameManager {
    [SerializeField] Player player;
    CollisionManager m_collisionManager;

    protected override void Awake()
    {
        player.Start();
    }

    protected override void Update()
    {
        // TODO: player graphics
        player.Update();
        m_collisionManager.Update();
    }

    protected override void OnUIManagerCreated() {

    }
}
