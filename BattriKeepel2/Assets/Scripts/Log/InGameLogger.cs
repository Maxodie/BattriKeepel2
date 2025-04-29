using UnityEngine;
using TMPro;

public class InGameLogger : MonoBehaviour
{
    [SerializeField] TMP_Text logText;

    void Awake()
    {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
        Log.SetInGameLogger(this);
#endif
    }

#if DEVELOPMENT_BUILD || UNITY_EDITOR
    public void SendLog(string text)
    {
        logText.text += text + "\n\n";
    }
#endif
}
