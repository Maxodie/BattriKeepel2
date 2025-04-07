using UnityEngine;

public class GameManagerLogger : Logger
{
}


class BossEntity : IGameEntity
{
    BossGraphics entityGraphics;
    int health = 100;

    public BossEntity(Transform spawnLocation, BossGraphics bossGrpahics)
    {
        entityGraphics = Object.Instantiate<BossGraphics>(bossGrpahics, spawnLocation);
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
    [SerializeField] Transform tr;
    [SerializeField] BossGraphics bg;
    int id;
    void Start()
    {
        id = EntityManager.GetInstance().CreateEntity<BossEntity>(out entity0, tr, bg);
        entity0.TakeDamage(25);
    }
}
