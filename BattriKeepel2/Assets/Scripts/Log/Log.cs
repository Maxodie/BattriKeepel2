using UnityEngine;
using System.Collections.Generic;

public abstract class Logger
{
    public bool IsActive{set;get;}
}

public class DefaultLogger: Logger
{

}

public static class Log
{
    static List<Logger> m_loggers;

    static string s_successColor = "#008714";
    static string s_traceColor = "#287a92";
    static string s_warnColor = "#e57f1f";
    static string s_errorColor = "#e51f1f";

    static Log()
    {
        m_loggers = new();
        CreateLogger<DefaultLogger>();
    }

    public static void CreateLogger<T>() where T: Logger, new()
    {
        Logger logger = m_loggers.Find(delegate(Logger item){ return item is T; });
        if(logger == null)
        {
            m_loggers.Add(new T());
            Log.Success($"Logger of type : {typeof(T)} has been added");
        }
        else
        {
            Log.Error($"Logger of type : {typeof(T)} already exist");
        }

    }

    public static void DestroyLogger<T>() where T: Logger
    {
        Logger logger = m_loggers.Find(delegate(Logger item){ return item is T; });
        if(logger != null)
        {
            m_loggers.Remove(logger);
            Log.Success($"Logger of type : {typeof(T)} has been removed");
        }
        else
        {
            Log.Error($"Could not find Logger of type : {typeof(T)}");
        }
    }

    public static void DeactivateLogger<T>() where T: Logger
    {
        Logger logger = m_loggers.Find(delegate(Logger item){ return item is T; });

        if(logger != null)
        {
            logger.IsActive = false;
            Log.Info($"Logger of type : {typeof(T)} has been deactivated");
        }
        else
        {
            Log.Warn($"Could not find logger of type : {typeof(T)}");
        }
    }

    public static void Info<T>(object msg) where T: Logger
    {
        LogToLogger<T>(LogType.Log, msg);
    }

    public static void Info(object msg)
    {
        LogToLogger<DefaultLogger>(LogType.Log, msg);
    }

    public static void Trace<T>(object msg) where T: Logger
    {
        LogToLogger<T>(LogType.Log, $"<color={s_traceColor}>{msg}</color>");
    }

    public static void Trace(object msg)
    {
        LogToLogger<DefaultLogger>(LogType.Log, $"<color={s_traceColor}>{msg}</color>");
    }

    public static void Success<T>(object msg) where T: Logger
    {
        LogToLogger<T>(LogType.Log, $"<color={s_successColor}>{msg}</color>");
    }

    public static void Success(object msg)
    {
        LogToLogger<DefaultLogger>(LogType.Log, $"<color={s_successColor}>{msg}</color>");
    }

    public static void Warn<T>(object msg) where T: Logger
    {
        LogToLogger<T>(LogType.Warning, $"<color={s_warnColor}>{msg}</color>");
    }

    public static void Warn(object msg)
    {
        LogToLogger<DefaultLogger>(LogType.Warning, $"<color={s_warnColor}>{msg}</color>");
    }

    public static void Error<T>(object msg) where T: Logger
    {
        LogToLogger<T>(LogType.Error, $"<color={s_errorColor}>{msg}</color>");
    }

    public static void Error(object msg)
    {
        LogToLogger<DefaultLogger>(LogType.Error, $"<color={s_errorColor}>{msg}</color>");
    }

    static void LogToLogger<T>(LogType logType, object msg) where T: Logger
    {
        Logger logger = m_loggers.Find(delegate(Logger item){ return item is T; });
        if(logger != null && logger.IsActive)
        {
            Debug.unityLogger.Log(logType, msg);
        }
        else
        {
            Debug.unityLogger.Log(LogType.Error, $"Logger of type ${typeof(T)} could not be found");
        }
    }
}
