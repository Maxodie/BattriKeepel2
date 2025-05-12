using UnityEngine;
using UnityEngine.UI;

public class UILevelLoadingScreen : UIMenuBase
{
    [SerializeField] Image loadingScreen;

    public void SetLoadingScreen(Sprite sprite)
    {
        loadingScreen.sprite = sprite;
    }
}
