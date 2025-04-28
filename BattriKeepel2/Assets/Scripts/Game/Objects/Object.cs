using Components;

public abstract class Object : GameEntityMonoBehaviour {
    public Hitbox m_hitBox;

    protected virtual void Start() {
        m_hitBox.Init(this.transform);
    }
}
