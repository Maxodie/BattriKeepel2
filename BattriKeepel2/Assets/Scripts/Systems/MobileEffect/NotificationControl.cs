using UnityEngine.Android;
using Unity.Notifications.Android;

public class NotificationControllLogger : Logger
{

}

public static class NotificationControl
{
    public static void SetupNotification()
    {
        if(IsAndroid())
        {
            if(!Permission.HasUserAuthorizedPermission("android.permission.POST_NOTIFICATIONS"))
            {
                Permission.RequestUserPermission("android.permission.POST_NOTIFICATIONS");
                Log.Info<MobileEffectLogger>("notification permission requested");
            }
            else
            {
                Log.Info<MobileEffectLogger>("notification permission already autorized");
            }
        }

        RegisterNotificationChannel();
    }

    static bool IsAndroid()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        return true;
#else
        return false;
#endif
    }

    public static void RegisterNotificationChannel()
    {
        if(IsAndroid())
        {
            var channel = new AndroidNotificationChannel
            {
                Id = "dialog_channel",
                   Name = "Dialog Channel",
                   Importance = Importance.Default,
                   Description = "Boss dialog"
            };
            AndroidNotificationCenter.RegisterNotificationChannel(channel);
        }
    }

    //set up notification template
    public static void SendNotification(SO_NotificationProfile notificationProfile, string title, string text)
    {
        if(IsAndroid())
        {
            AndroidNotification notification = new();
            notification.Title = title;
            notification.Text = text;
            notification.FireTime = System.DateTime.Now.AddMilliseconds(notificationProfile.fireTimeInMs);
            notification.SmallIcon = notificationProfile.smallIconIdentifier;
            notification.LargeIcon = notificationProfile.bigIconIdentifier;

            AndroidNotificationCenter.SendNotification(notification, "dialog_channel");
            Log.Info<NotificationControllLogger>("Notification Sent with title : " + title);
        }
    }
}
