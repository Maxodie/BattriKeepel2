using UnityEngine;

public class GameManagerLogger : Logger
{
}

class BossEntity : IGameEntity
{
    BossGraphics entityGraphics;
    public int health = 100;

    public BossEntity(Transform spawnLocation, BossGraphics bossGrpahics)
    {
        entityGraphics = GraphicsManager.Get().GenerateVisualInfos<BossGraphics>(bossGrpahics, spawnLocation);
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        entityGraphics.SetLifeAmount(health / 100.0f);
    }

}

public class GameManager : GameEntityMonoBehaviour
{
    BossEntity entity0;
    BossEntity entity1;
    BossEntity entity2;
    [SerializeField] Transform tr;
    [SerializeField] BossGraphics bg;
    int id;
    int id1;
    int id2;
    void Start()
    {
        id = EntityManager.Get().CreatePersistentEntity(out entity0, tr, bg);
        entity0.TakeDamage(25);
        id1 = EntityManager.Get().CreatePersistentEntity<BossEntity>(out entity1, tr, bg);
        id2 = EntityManager.Get().CreatePersistentEntity<BossEntity>(out entity2, tr, bg);

        entity0.TakeDamage(25);
        entity1.TakeDamage(25);

        BossEntity get = EntityManager.Get().GetEntity<BossEntity>(id);
        Log.Info<GameManagerLogger>("entity 0 hp : " + get.health);

        BossEntity get1 = EntityManager.Get().GetEntity<BossEntity>(id1);
        Log.Info<GameManagerLogger>("entity 1 hp : " + get1.health);

        BossEntity get2 = EntityManager.Get().GetEntity<BossEntity>(id2);
        Log.Info<GameManagerLogger>("entity 2 hp : " + get2.health);
    }
}
