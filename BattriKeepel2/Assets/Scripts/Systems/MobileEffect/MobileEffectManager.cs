using UnityEngine;

#if UNITY_ANDROID
using UnityEngine.Android;
#endif

public class MobileEffectLogger : Logger
{

}

public enum MobileEffectVibration
{
    SMALL = 250,
    MEDIUM = 450,
    BIG = 650
}

public static class MobileEffect
{
#if UNITY_ANDROID && !UNITY_EDITOR
    static AndroidJavaClass m_unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    static AndroidJavaObject m_currentActivity = m_unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

    static AndroidJavaObject m_vibrator = m_currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");

    static AndroidJavaObject m_cameraService = m_currentActivity.Call<AndroidJavaObject>("getSystemService", "camera");
    static string[] m_cameraIDList = m_cameraService.Call<string[]>("getCameraIdList");

#else
    static AndroidJavaObject m_vibrator;

    static string[] m_cameraIDList;
    static AndroidJavaObject m_cameraService;
#endif

    static Awaitable m_flashWaitForEnd;

    public static void SetupPermissions()
    {
        if(IsAndroid())
        {
            string[] perms = {Permission.Camera};
            Permission.RequestUserPermissions(perms);
        }
    }

    static bool IsAndroid()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        return true;
#else
        return false;
#endif
    }

    public static void VibrationEffect(MobileEffectVibration vibrationType)
    {
        if(IsAndroid())
        {
            m_vibrator.Call("vibrate", (long)vibrationType);
        }
        else
        {
            Handheld.Vibrate();
        }

        Log.Info<MobileEffectLogger>("start vibration " + vibrationType.ToString());
    }

    public static void CancelVibration()
    {
        if(IsAndroid())
        {
            m_vibrator.Call("cancel");
            Log.Info<MobileEffectLogger>("cancel vibration");
        }
    }

    public static void SetOnFlashlight(bool state, float durationS)
    {
        if(IsAndroid())
        {
            if(m_cameraIDList.Length != 0)
            {
                m_cameraService.Call("setTorchMode", (m_cameraIDList.Length - 1).ToString(), state ? true : false);

                if(m_flashWaitForEnd != null && !m_flashWaitForEnd.IsCompleted)
                {
                    m_flashWaitForEnd.Cancel();
                }

                m_flashWaitForEnd = WaitForFlashToEnd(durationS);
            }
        }
    }

    static async Awaitable WaitForFlashToEnd(float durationS)
    {
        await Awaitable.WaitForSecondsAsync(durationS);
        if(IsAndroid())
        {
            if(m_cameraIDList.Length != 0)
            {
                m_cameraService.Call("setTorchMode", "0", false);
            }
        }
    }
}
