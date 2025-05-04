using UnityEngine;

[CreateAssetMenu(fileName = "DialogData", menuName = "Dialog/DialogData")]
public class SO_DialogData : ScriptableObject
{
    public SO_NotificationProfile notificationProfile;
    public DialogSentence[] dialogSentences;
}

[System.Serializable]
public class DialogSentence
{
    public string title;
    public string content;
}
