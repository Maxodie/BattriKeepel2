using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FrogFarmUIMenu : UIMenuBase
{
    [SerializeField] Image m_xpFillAmount;
    [SerializeField] TMP_Text m_xmAmountText;
    [SerializeField] TMP_Text m_levelText;

    public void SetXpFill(int currentAmount, int threasholdAmount, int level)
    {
        m_xpFillAmount.fillAmount = currentAmount / (float)threasholdAmount;
        m_xmAmountText.text = $"{currentAmount} / {threasholdAmount}";
        m_levelText.text = $"LVL : {level}";
    }
}
