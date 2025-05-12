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
using System.Diagnostics;
using System;
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
#if DEVELOPMENT_BUILD || UNITY_EDITOR
    public static int s_cacaCount = 0;
    static StackTrace stackTrace = new StackTrace();
    static Dictionary<string, int> s_cacaCountDic = new();

    public static List<Logger> m_loggers;
    public static UnityEvent<Logger> m_onLoggerCreated = new();

    static string s_successColor = "#008714";
    static string s_traceColor = "#287a92";
    static string s_warnColor = "#e57f1f";
    static string s_errorColor = "#e51f1f";
    static string s_loggerColor = "#7decf0";

    static InGameLogger s_inGameLogger;
#endif


    static Log()
    {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
        m_loggers = new();
        Log.CreateLogger<DefaultLogger>();
#endif
    }

#if DEVELOPMENT_BUILD || UNITY_EDITOR
    public static void SetInGameLogger(InGameLogger inGamelogger)
    {
        s_inGameLogger = inGamelogger;
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
#endif

    public static void DestroyLogger<T>() where T: Logger
    {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
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
#endif
    }

    public static void DeactivateLogger<T>() where T: Logger, new()
    {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
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
#endif
    }

    public static void Info(object msg=null)
    {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
        LogToLogger<DefaultLogger>(LogType.Log, s_traceColor, msg);
#endif
    }

    public static void Info<T>(object msg=null) where T: Logger, new()
    {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
        LogToLogger<T>(LogType.Log, s_traceColor, msg);
#endif
    }

    public static void Trace<T>(object msg=null) where T: Logger, new()
    {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
        LogToLogger<T>(LogType.Log, s_traceColor, msg);
#endif
    }

    public static void Trace(object msg=null)
    {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
        LogToLogger<DefaultLogger>(LogType.Log, s_traceColor, msg);
#endif
    }

    public static void Success<T>(object msg=null) where T: Logger, new()
    {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
        LogToLogger<T>(LogType.Log, s_successColor, msg);
#endif
    }

    public static void Success(object msg=null)
    {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
        LogToLogger<DefaultLogger>(LogType.Log, s_successColor, msg);
#endif
    }

    public static void Warn<T>(object msg=null) where T: Logger, new()
    {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
        LogToLogger<T>(LogType.Warning, s_warnColor, msg);
#endif
    }

    public static void Warn(object msg=null)
    {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
        LogToLogger<DefaultLogger>(LogType.Warning, s_warnColor, msg);
#endif
    }

    public static void Error<T>(object msg=null) where T: Logger, new()
    {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
        LogToLogger<T>(LogType.Error, s_errorColor, msg);
#endif
    }

    public static void Error(object msg=null)
    {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
        LogToLogger<DefaultLogger>(LogType.Error, s_errorColor, msg);
#endif
    }

    static void LogToLogger<T>(LogType logType, string color, object msg) where T: Logger, new()
    {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
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
                string trace = Environment.StackTrace;
                if(s_cacaCountDic.ContainsKey(trace))
                {
                    msg = "caca" + s_cacaCountDic[trace];
                }
                else
                {
                    s_cacaCountDic.Add(trace, ++s_cacaCount);
                    msg = "caca" + s_cacaCountDic[trace];
                }
            }

            string finalLog = $"<color={s_loggerColor}>[{logger.GetType().FullName}]</color> <{color}>{msg}</color>";
            UnityEngine.Debug.unityLogger.Log(logType, finalLog);
            if(s_inGameLogger)
            {
                s_inGameLogger.SendLog(finalLog);
            }
        }
#endif
    }
}
