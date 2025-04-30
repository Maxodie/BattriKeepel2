using UnityEngine;
using UnityEngine.Android;
using System.Collections;

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

    static AndroidJavaClass m_cameraClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    static AndroidJavaObject m_camera = m_cameraClass.GetStatic<AndroidJavaObject>("currentActivity");
    static AndroidJavaObject m_cameraService = m_camera.Call<AndroidJavaObject>("getSystemService", "camera");
    static AndroidJavaObject m_cameraList = m_camera.Call<AndroidJavaObject>("getSystemService", "camera");

#else
    static AndroidJavaClass m_unityPlayer;
    static AndroidJavaObject m_currentActivity;
    static AndroidJavaObject m_vibrator;

    static AndroidJavaClass m_cameraClass;
    static AndroidJavaObject m_camera;
    static AndroidJavaObject m_cameraService;
#endif

    static bool m_hasPersmission = false;
    public static WebCamDevice[] devices = WebCamTexture.devices;

    static void SetupPermissions()
    {
        string[] perms = {Permission.Camera};
        Permission.RequestUserPermissions(perms);
    }

    static void CheckPermissions()
    {
        if(!m_hasPersmission)
        {
            SetupPermissions();
            m_hasPersmission = true;
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
        }
        Log.Info<MobileEffectLogger>("cancel vibration ");
    }

    public static void SetOnFlashlight(bool state)
    {
        CheckPermissions();

        if(IsAndroid())
        {
                Log.Info<MobileEffectLogger>("active flashlight + " + devices.Length);
                m_cameraService.Call("setTorchMode", 0, state ? true : false);
            if(devices.Length != 0)
            {
            }
        }
    }
}
