using UnityEngine.UI;
using UnityEngine;

public class BossGraphics : GameEntityGraphics
{
    [SerializeField] Image lifefill;

    public void SetLifeAmount(float amount)
    {
        lifefill.fillAmount = amount;
    }
}
