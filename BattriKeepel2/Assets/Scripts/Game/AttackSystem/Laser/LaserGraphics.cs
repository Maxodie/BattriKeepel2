using UnityEngine;

public class LaserGraphics : GameEntityGraphics {
    [SerializeField] GameObject m_fillIn;
    [SerializeField] GameObject m_laser;

    void Start() {
        m_laser.SetActive(false);
        Vector3 fillInCurrentScale = m_fillIn.transform.localScale;
        m_fillIn.transform.localScale = new Vector3(0, fillInCurrentScale.y, fillInCurrentScale.z);
    }

    public void SetFillInSize(float amount) {
        Vector3 fillInCurrentScale = m_fillIn.transform.localScale;
        m_fillIn.transform.localScale = new Vector3(amount, fillInCurrentScale.y, fillInCurrentScale.z);
    }

    public void TriggerLaser() {
        m_laser.SetActive(true);
    }
}
