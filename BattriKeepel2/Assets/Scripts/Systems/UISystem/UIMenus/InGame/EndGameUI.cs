using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGameUI : UIMenuBase
{
    [SerializeField] TMP_Text title;
    [SerializeField] Color winColor;
    [SerializeField] Color loseColor;
    [SerializeField] SO_LevelData levelData;

    [Header("Frog")]
    [SerializeField] TMP_Text frogName;
    [SerializeField] Image frogVisual;
    public void SetWinState(bool state)
    {
        if(state)
        {
            title.text = "GG, your cool";
            title.color = winColor;
        }
        else
        {
            title.text = "You loose";
            title.color = loseColor;
        }
    }

    public void SetNewFrogDataInfos(FrogDynamicData frogData)
    {
        frogName.text = frogData.m_frogName;
        frogVisual.sprite = FrogGenerator.Get().QueryVisual(frogData.m_rarity);
    }

    public void GoToMainMenu()
    {
        LevelLoader.LoadLevel(levelData);
    }
}
