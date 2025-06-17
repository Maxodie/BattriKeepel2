using TMPro;
using UnityEngine;

public class EndGameUI : UIMenuBase
{
    [SerializeField] TMP_Text title;
    [SerializeField] Color winColor;
    [SerializeField] Color loseColor;
    [SerializeField] SO_LevelData levelData;
    public void SetWinState(bool state)
    {
        if(state)
        {
            title.text = "GG, your cool";
            title.color = winColor;
        }
        else
        {
            title.text = "L + Ratio + loser";
            title.color = loseColor;
        }
    }

    public void GoToMainMenu()
    {
        LevelLoader.LoadLevel(levelData);
    }
}
