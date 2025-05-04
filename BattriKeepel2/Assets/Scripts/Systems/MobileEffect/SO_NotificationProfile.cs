using UnityEngine;

[CreateAssetMenu(fileName = "NotificationProfile", menuName = "Notification/NotificationProfile")]
public class SO_NotificationProfile : ScriptableObject
{
    public string smallIconIdentifier;
    public string bigIconIdentifier;
    public int fireTimeInMs;
}
