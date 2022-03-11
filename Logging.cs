#nullable enable

using UnityEngine; // UnityEngine.Object
using Edanoue.Logging.Internal;

namespace Edanoue.Logging
{
    using ILogger = Interfaces.ILogger;

    /// <summary>
    /// Utility functions at module level.
    /// Basically delegate everything to the root logger.
    /// </summary>
    public static class Logging
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Logger GetLogger(string name)
        {
            // FIXME
            // For Unity Gameobject arguments API
            return (Logger)Manager.GetLogger(name);
        }

        /// <summary>
        /// Set global (root) logger config 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="level"></param>
        public static void SetLevel(LogLevel level)
        {
            Manager.Root.SetLevel(level);
        }

        /// <summary>
        /// Log message with severity "Debug" from root logger.
        /// </summary>
        /// <param name="message"></param>
        public static void Debug(string message)
        {
            Manager.Root.Debug(message);
        }

        public static void Debug(string message, UnityEngine.Object context)
        {
            Manager.Root.Debug(message, context);
        }

        /// <summary>
        /// Log message with severity "Info" from root logger.
        /// </summary>
        /// <param name="message"></param>
        public static void Info(string message)
        {
            Manager.Root.Info(message);
        }

        public static void Info(string message, UnityEngine.Object context)
        {
            Manager.Root.Info(message, context);
        }

        /// <summary>
        /// Log message with severity "Warning" from root logger.
        /// </summary>
        /// <param name="message"></param>
        public static void Warning(string message)
        {
            Manager.Root.Warning(message);
        }

        public static void Warning(string message, UnityEngine.Object context)
        {
            Manager.Root.Warning(message, context);
        }

        /// <summary>
        /// Log message with severity "Error" from root logger.
        /// </summary>
        /// <param name="message"></param>
        public static void Error(string message)
        {
            Manager.Root.Error(message);
        }

        public static void Error(string message, UnityEngine.Object context)
        {
            Manager.Root.Error(message, context);
        }

        /// <summary>
        /// Log message with severity "Critical" from root logger.
        /// </summary>
        /// <param name="message"></param>
        public static void Critical(string message)
        {
            Manager.Root.Critical(message);
        }

        public static void Critical(string message, UnityEngine.Object context)
        {
            Manager.Root.Critical(message, context);
        }

        public static void Log(int level, string message)
        {
            Manager.Root.Log(level, message);
        }

        public static void Log(int level, string message, UnityEngine.Object context)
        {
            Manager.Root.Log(level, message, context);
        }
    }
}