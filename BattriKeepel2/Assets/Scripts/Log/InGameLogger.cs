using UnityEngine;
using TMPro;

public class InGameLogger : MonoBehaviour
{
    [SerializeField] TMP_Text logText;

    void Awake()
    {

    }

    public void SendLog(string text)
    {
        logText.text += text + "\n\n";
    }
}
