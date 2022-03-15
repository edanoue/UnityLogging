#nullable enable

using System.Collections.Generic;
using Edanoue.Logging.Interfaces;
using Edanoue.Logging.Internal;
using UnityEngine;
using ILogger = Edanoue.Logging.Interfaces.ILogger;

namespace Edanoue.Logging
{
    using Extra = KeyValuePair<string, object>;

    /// <summary>
    /// Utility functions at module level.
    /// Basically delegate everything to the root logger.
    /// </summary>
    public static class Logging
    {
        /// <summary>
        /// Return a logger with the specified name or, if name is None, return a logger which is the root logger of the
        /// hierarchy.
        /// <param name="name">
        /// If specified, the name is typically a dot-separated hierarchical
        /// name like "a", "a.b" or "a.b.c.d". Choice of these names is entirely up to the developer who is using logging.
        /// </param>
        /// <returns>logger</returns>
        /// </summary>
        public static ILogger GetLogger(string name)
        {
            if (string.IsNullOrEmpty(name) || name == CONST.ROOT_LOGGER_NAME)
                return Manager.Root;
            return Manager.GetLogger(name);
        }

        /// <summary>
        /// Return a logger with the specified class, creating it if necessary.
        /// </summary>
        /// <note>
        /// GetLogger&lt;"MyClass"&gt;() is same to  GetLogger("MyNamespace.MyClass")
        /// </note>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static ILogger GetLogger<T>()
        {
            // Get Type fullname
            var name = typeof(T).FullName;
            // Replace nested type separator "+" to "."
            name = name.Replace("+", CONST.NAME_SEPARATOR);
            return Manager.GetLogger(name);
        }

        /// <summary>
        /// Return a logger with the specified class, creating it if necessary.
        /// </summary>
        /// <note>
        /// GetLogger&lt;"MyClass"&gt;("Foo") is same to GetLogger("MyNamespace.MyClass.Foo")
        /// </note>
        /// <param name="childName">child logger name</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static ILogger GetLogger<T>(string childName)
        {
            // Get Type fullname
            var name = typeof(T).FullName;
            // Replace nested type separator "+" to "."
            name = name.Replace("+", CONST.NAME_SEPARATOR);
            // Add name
            name += $"{CONST.NAME_SEPARATOR}{childName}";
            return Manager.GetLogger(name);
        }

        /// <summary>
        /// Set the logging level of root logger
        /// </summary>
        /// <param name="level"></param>
        public static void SetLevel(LogLevel level)
        {
            Manager.Root.SetLevel(level);
        }

        /// <summary>
        /// Log message with severity "Debug" on root logger.
        /// </summary>
        /// <param name="message"></param>
        public static void Debug(string message)
        {
            Manager.Root.Debug(message);
        }

        /// <summary>
        /// Log message with severity "Debug" on root logger
        /// with UnityEngine.Object context
        /// </summary>
        /// <param name="message"></param>
        /// <param name="context"></param>
        public static void Debug(string message, Object context)
        {
            Manager.Root.Debug(message, new Extra(CONST.UNITY_CONTEXT_KEY, context));
        }

        /// <summary>
        /// Log message with severity "Info" on root logger.
        /// </summary>
        /// <param name="message"></param>
        public static void Info(string message)
        {
            Manager.Root.Info(message);
        }

        /// <summary>
        /// Log message with severity "Info" on root logger.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="context"></param>
        public static void Info(string message, Object context)
        {
            Manager.Root.Info(message, new Extra(CONST.UNITY_CONTEXT_KEY, context));
        }

        /// <summary>
        /// Log message with severity "Warning" on root logger.
        /// </summary>
        /// <param name="message"></param>
        public static void Warning(string message)
        {
            Manager.Root.Warning(message);
        }

        /// <summary>
        /// Log message with severity "Warning" on root logger.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="context"></param>
        public static void Warning(string message, Object context)
        {
            Manager.Root.Warning(message, new Extra(CONST.UNITY_CONTEXT_KEY, context));
        }

        /// <summary>
        /// Log message with severity "Error" on root logger.
        /// </summary>
        /// <param name="message"></param>
        public static void Error(string message)
        {
            Manager.Root.Error(message);
        }

        /// <summary>
        /// Log message with severity "Error" on root logger.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="context"></param>
        public static void Error(string message, Object context)
        {
            Manager.Root.Error(message, new Extra(CONST.UNITY_CONTEXT_KEY, context));
        }

        /// <summary>
        /// Log message with severity "Critical" on root logger.
        /// </summary>
        /// <param name="message"></param>
        public static void Critical(string message)
        {
            Manager.Root.Critical(message);
        }

        /// <summary>
        /// Log message with severity "Critical" on root logger.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="context"></param>
        public static void Critical(string message, Object context)
        {
            Manager.Root.Critical(message, new Extra(CONST.UNITY_CONTEXT_KEY, context));
        }

        /// <summary>
        /// Log message with the integer severity level on root logger.
        /// </summary>
        /// <param name="level"></param>
        /// <param name="message"></param>
        public static void Log(int level, string message)
        {
            Manager.Root.Log(level, message);
        }

        /// <summary>
        /// Log message with the integer severity level on root logger.
        /// </summary>
        /// <param name="level">user defined level</param>
        /// <param name="message"></param>
        /// <param name="context"></param>
        public static void Log(int level, string message, Object context)
        {
            Manager.Root.Log(level, message, new Extra(CONST.UNITY_CONTEXT_KEY, context));
        }

        /// <summary>
        /// Is root logger enabled for level?
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public static bool IsEnabledFor(int level)
        {
            return Manager.Root.IsEnabledFor(level);
        }

        public static bool IsEnabledFor(LogLevel level)
        {
            return IsEnabledFor((int) level);
        }

        /// <summary>
        /// Add the specified handler to root logger.
        /// </summary>
        /// <param name="handler"></param>
        public static void AddHandler(IHandler handler)
        {
            Manager.Root.AddHandler(handler);
        }
    }
}