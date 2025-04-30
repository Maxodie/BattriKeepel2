using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class InGameLogger : MonoBehaviour
{
#if DEVELOPMENT_BUILD || UNITY_EDITOR
    [SerializeField] TMP_Text m_logText;
    [SerializeField] int m_maxCharCount;
    List<int> m_currentMsgLengths = new();
#endif

    void Awake()
    {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
        Log.SetInGameLogger(this);
#endif
    }

#if DEVELOPMENT_BUILD || UNITY_EDITOR
    public void SetActiveLog(bool state)
    {
        gameObject.SetActive(state);
    }

    public void SendLog(string text)
    {

        string finalText = text + "\n";
        if(finalText.Length >= m_maxCharCount)
        {
            Log.Warn<DebuggerLogger>("failed to draw a log, please make its Length less than " + m_maxCharCount);
        }

        while(m_logText.text.Length + finalText.Length > m_maxCharCount)
        {
            m_logText.text = m_logText.text.Remove(0, m_currentMsgLengths[0]);
            UpdateLogMsgLength(finalText.Length);

        }

        m_logText.text += finalText;
        m_currentMsgLengths.Add(finalText.Length);
    }

    void UpdateLogMsgLength(int logLength)
    {
        for(int i = 1; i < m_currentMsgLengths.Count; i++)
        {
            m_currentMsgLengths[i - 1] = m_currentMsgLengths[i];
        }

        m_currentMsgLengths[m_currentMsgLengths.Count - 1] = logLength;
    }
#endif
}
