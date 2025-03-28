using UnityEngine;
using UnityEngine.UI;

public class DebuggerNavigatorTab : MonoBehaviour
{
    [SerializeField] Button navigationTabButton;

    public void AddNavigationTabBtnListener(UnityEngine.Events.UnityAction action)
    {
        navigationTabButton.onClick.AddListener(action);
    }
}
