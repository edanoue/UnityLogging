#nullable enable

using UnityEngine; // UnityEngine.Object

namespace Edanoue.Logging
{
    public static class Logging
    {
        static readonly RootLogger _rootLogger;

        static Logging()
        {
            // Create new root logger
            _rootLogger = new RootLogger((int)LogLevel.NotSet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Logger GetLogger(string name)
        {
            return _rootLogger.GetLogger(name);
        }

        /// <summary>
        /// Set global (root) logger config 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="level"></param>
        public static void SetLevel(LogLevel level)
        {
            _rootLogger.SetLevel(level);
        }

        /// <summary>
        /// Log message with severity "Debug" from root logger.
        /// </summary>
        /// <param name="message"></param>
        public static void Debug(string message)
        {
            _rootLogger.Debug(message);
        }

        public static void Debug(string message, UnityEngine.Object context)
        {
            _rootLogger.Debug(message, context);
        }

        /// <summary>
        /// Log message with severity "Info" from root logger.
        /// </summary>
        /// <param name="message"></param>
        public static void Info(string message)
        {
            _rootLogger.Info(message);
        }

        public static void Info(string message, UnityEngine.Object context)
        {
            _rootLogger.Info(message, context);
        }

        /// <summary>
        /// Log message with severity "Warning" from root logger.
        /// </summary>
        /// <param name="message"></param>
        public static void Warning(string message)
        {
            _rootLogger.Warning(message);
        }

        public static void Warning(string message, UnityEngine.Object context)
        {
            _rootLogger.Warning(message, context);
        }

        /// <summary>
        /// Log message with severity "Error" from root logger.
        /// </summary>
        /// <param name="message"></param>
        public static void Error(string message)
        {
            _rootLogger.Error(message);
        }

        public static void Error(string message, UnityEngine.Object context)
        {
            _rootLogger.Error(message, context);
        }

        /// <summary>
        /// Log message with severity "Critical" from root logger.
        /// </summary>
        /// <param name="message"></param>
        public static void Critical(string message)
        {
            _rootLogger.Critical(message);
        }

        public static void Critical(string message, UnityEngine.Object context)
        {
            _rootLogger.Critical(message, context);
        }

    }
}