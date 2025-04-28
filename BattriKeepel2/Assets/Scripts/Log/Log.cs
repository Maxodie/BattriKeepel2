/*
 *
 * pour créeer un logger il faut faire une classe enfant de 'Logger'
 * exemple :
 * public class DefaultLogger: Logger
 * {
 *
 * }
 *
 * pour utiliser le logger
 * Log.{LogLevel}<LeNouveauLogger>("hello");
 *
 * exemple complet :
 *
 * public class TestLogger: Logger
 * {
 *
 * }
 *
 * Log.Success<TestLogger>("caca");
 *
 */

using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public abstract class Logger
{
    [DebuggerToolAccess] public bool IsActive{set;get;}

    public virtual void OnCreated()
    {
        IsActive = true;
    }

    public virtual void OnLogStart()
    {

    }
}

public class DefaultLogger: Logger
{
}

public static class Log
{
    public static int m_cacaCount = 1;
    public static List<Logger> m_loggers;
    public static UnityEvent<Logger> m_onLoggerCreated = new();

    static string s_successColor = "#008714";
    static string s_traceColor = "#287a92";
    static string s_warnColor = "#e57f1f";
    static string s_errorColor = "#e51f1f";
    static string s_loggerColor = "#7decf0";

    static Log()
    {
        m_loggers = new();
        Log.CreateLogger<DefaultLogger>();
    }

    public static Logger CreateLogger<T>() where T: Logger, new()
    {
        Logger logger = m_loggers.Find(delegate(Logger item){ return item is T; });
        if(logger == null)
        {
            T t = new T();
            m_loggers.Add(t);
            t.OnCreated();
            m_onLoggerCreated.Invoke(t);
            return t;
        }
        else
        {
            Log.Error($"Logger of type : {typeof(T)} already exist");
            return null;
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

    public static void DeactivateLogger<T>() where T: Logger, new()
    {
        Logger logger = m_loggers.Find(delegate(Logger item){ return item is T; });

        if(logger != null)
        {
            logger.IsActive = false;
            Log.Info<T>($"Logger of type : {typeof(T)} has been deactivated");
        }
        else
        {
            Log.Warn<T>($"Could not find logger of type : {typeof(T)}");
        }
    }

    public static void Info(object msg=null)
    {
        LogToLogger<DefaultLogger>(LogType.Log, msg);
    }

    public static void Info<T>(object msg=null) where T: Logger, new()
    {
        LogToLogger<T>(LogType.Log, msg);
    }

    public static void Trace<T>(object msg=null) where T: Logger, new()
    {
        LogToLogger<T>(LogType.Log, $"<color={s_traceColor}>{msg}</color>");
    }

    public static void Trace(object msg=null)
    {
        LogToLogger<DefaultLogger>(LogType.Log, $"<color={s_traceColor}>{msg}</color>");
    }

    public static void Success<T>(object msg=null) where T: Logger, new()
    {
        LogToLogger<T>(LogType.Log, $"<color={s_successColor}>{msg}</color>");
    }

    public static void Success(object msg=null)
    {
        LogToLogger<DefaultLogger>(LogType.Log, $"<color={s_successColor}>{msg}</color>");
    }

    public static void Warn<T>(object msg=null) where T: Logger, new()
    {
        LogToLogger<T>(LogType.Warning, $"<color={s_warnColor}>{msg}</color>");
    }

    public static void Warn(object msg=null)
    {
        LogToLogger<DefaultLogger>(LogType.Warning, $"<color={s_warnColor}>{msg}</color>");
    }

    public static void Error<T>(object msg=null) where T: Logger, new()
    {
        LogToLogger<T>(LogType.Error, $"<color={s_errorColor}>{msg}</color>");
    }

    public static void Error(object msg=null)
    {
        LogToLogger<DefaultLogger>(LogType.Error, $"<color={s_errorColor}>{msg}</color>");
    }

    static void LogToLogger<T>(LogType logType, object msg) where T: Logger, new()
    {
        Logger logger = m_loggers.Find(delegate(Logger item){ return item is T; });

        if(logger == null)
        {
            logger = CreateLogger<T>();
            Log.Info<DefaultLogger>("logger created : " + logger.ToString());
        }

        logger.OnLogStart();

        if(logger.IsActive)
        {
            if(msg == null)
            {
                msg = "caca" + m_cacaCount++;
            }

            Debug.unityLogger.Log(logType, $"<color={s_loggerColor}>[" + logger.GetType().FullName + "]</color> " + msg);
        }
    }
}
