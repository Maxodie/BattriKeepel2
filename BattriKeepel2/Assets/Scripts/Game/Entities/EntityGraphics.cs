using Game.Entities;

public class EntityGraphics : GameEntityGraphics
{
    public virtual void TakeDamage(float amount)
    {
        ((Entity)GetOwner()).TakeDamage(amount);
    }
}
