using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DebuggerNavigatorTab : MonoBehaviour
{
    [SerializeField] Button navigationTabButton;
    [SerializeField] TMP_Text textTitle;

    public void SetNavigatorTabTitle(string title)
    {
        string[] strings = title.Split("UI");
        textTitle.text = strings.Length > 1 ? strings[1] : strings[0];
    }

    public void AddNavigationTabBtnListener(UnityEngine.Events.UnityAction action)
    {
        navigationTabButton.onClick.AddListener(action);
    }
}
