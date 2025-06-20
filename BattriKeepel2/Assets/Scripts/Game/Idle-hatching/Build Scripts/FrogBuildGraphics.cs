using UnityEngine;
using UnityEngine.UI;

public class FrogBuildGraphics : GameEntityGraphics
{
    public float buildDuration = 10;
    [SerializeField] Image m_progressFill;
    [SerializeField] GameObject m_progressVisual;

    public void SetActiveProgress(bool state)
    {
        m_progressVisual.SetActive(state);
    }

    public void SetFrogFill(float amount)
    {
        m_progressFill.fillAmount = amount;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Frog frog = col.GetComponent<FrogGraphics>().GetFrog();
        if(frog.IsInInteraction())
        {
            GetOwner<FrogBuild>().StartWorking(frog);
        }
    }
}
