using UnityEngine;

public class DialogComponent
{
    int m_currentSentence = 0;

    Awaitable m_currentSentenceWait = null;

    public DialogComponent()
    {
        m_currentSentence = 0;
    }

    public void StartDialog(SO_DialogData data)
    {
        m_currentSentenceWait = StartNextSentence(data);
    }

    public async Awaitable StartNextSentence(SO_DialogData data)
    {
        MobileEffect.VibrationEffect(MobileEffectVibration.SMALL);
        NotificationControl.SendNotification(data.notificationProfile, data.dialogSentences[m_currentSentence].title, data.dialogSentences[m_currentSentence].content);
        await Awaitable.WaitForSecondsAsync(data.durationInTime);

        m_currentSentence++;
        if(data.dialogSentences.Length > m_currentSentence)
        {
            m_currentSentenceWait = StartNextSentence(data);
        }
        else
        {
            m_currentSentenceWait = null;
        }
    }
}
