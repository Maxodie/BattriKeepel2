using UnityEngine;

public static class Log
{
    static string s_successColor = "#008714";
    static string s_traceColor = "#287a92";

    public static void Info(object msg)
    {
        Debug.unityLogger.Log(LogType.Log, msg);
    }

    public static void Succes(object msg)
    {

        Debug.unityLogger.Log(LogType.Log, $"<color={s_successColor}>{msg}</color>");
    }

    public static void Error(object msg)
    {
        Debug.unityLogger.Log(LogType.Error, msg);
    }

    public static void Trace(object msg)
    {

        Debug.unityLogger.Log(LogType.Log, $"<color={s_traceColor}>{msg}</color>");
    }
}
