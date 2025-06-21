using UnityEngine;

public class LaserGraphics : GameEntityGraphics {
    [SerializeField] GameObject m_fillIn;
    [SerializeField] GameObject m_laser;
    [SerializeField] Collider2D m_collider;

    void Start() {
        m_collider.enabled = false;
        m_laser.SetActive(false);
        Vector3 fillInCurrentScale = m_fillIn.transform.localScale;
        m_fillIn.transform.localScale = new Vector3(0, fillInCurrentScale.y, fillInCurrentScale.z);
    }

    public void SetFillInSize(float amount) {
        Vector3 fillInCurrentScale = m_fillIn.transform.localScale;
        m_fillIn.transform.localScale = new Vector3(amount, fillInCurrentScale.y, fillInCurrentScale.z);
    }

    public void TriggerLaser() {
        if (m_laser.activeSelf) {
            return;
        }

        m_collider.enabled = true;
        m_laser.SetActive(true);
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        EntityGraphics entityGraphics = col.gameObject.GetComponent<EntityGraphics>();
        if(entityGraphics && entityGraphics.GetOwner().GetType() == typeof(GameEntity.Player))
        {
            entityGraphics.TakeDamage(1);
        }
    }
}
