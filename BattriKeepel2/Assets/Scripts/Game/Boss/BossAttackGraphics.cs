public abstract class BossAttackParent : IGameEntity {

    public bool isActive = true;
    public abstract void Init(BossEntity boss, SO_BossAttackData data, AttackGraphicsPool graphicsPool, GameEntity.Player player);
    public virtual void Update(){}
    public abstract void Clean();
}
